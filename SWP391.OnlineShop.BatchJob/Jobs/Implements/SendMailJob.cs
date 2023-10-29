using Hangfire;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SWP391.OnlineShop.BatchJob.Jobs.Interfaces;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Customs.Environments;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Settings;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using System.Text;
using options = Microsoft.Extensions.Options;

namespace SWP391.OnlineShop.BatchJob.Jobs.Implements
{
    public class SendMailJob : IJobWebService
    {
        private readonly ILoggerService _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly options.IOptions<DeveloperEnvironment> _developerEnv;
        private readonly options.IOptions<Smtp> _smtp;

        public string JobName => "Send Mail Job - P3";
        public string CronExpression => "*/3 * * * *";

        public SendMailJob(
            ILoggerService logger,
            IUnitOfWork unitOfWork,
            options.IOptions<DeveloperEnvironment> developerEnv,
            options.IOptions<Smtp> smtp)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _developerEnv = developerEnv;
            _smtp = smtp;
        }

        [AutomaticRetry(Attempts = 0)]
        [DisableConcurrentExecution(10)]
        public void RunJob()
        {
            _logger.LogInfo("SendMailJob Start: " + DateTime.Now);

            try
            {
                var currentTime = DateTime.Now;

                //Detect Expired Jobs 
                var expiredMails = _unitOfWork.Context.Emails?.Where(e => e.MailStatus == MailStatus.Sending &&
                                            EF.Functions.DateDiffMinute(e.ModifiedDateTime, currentTime) >= 10);

                if (expiredMails != null && expiredMails.Any())
                {
                    foreach (var expiredMail in expiredMails)
                    {
                        expiredMail.MailStatus = MailStatus.Expired;
                        _unitOfWork.Context.Emails?.Update(expiredMail);
                    }
                }

                //Get all mail jobs for which is new or expired or failed with retry counter less than or equal to 15.
                var mails = _unitOfWork.Context.Emails?.Where(e =>
                    ((e.MailStatus == MailStatus.Expired || e.MailStatus == MailStatus.Failed) && e.RetryCounter < 15) ||
                    e.MailStatus == MailStatus.New)
                    .OrderBy(e => e.CreatedDateTime);


                var prepareSendingEmails = new List<Email>();

                if (mails != null && mails.Any())
                {
                    // Assuming default SMTP rate limit is 500 emails / hour. 
                    // The job run every 3 minutes. 
                    // 500 / 60 * 3 = 25
                    foreach (var mail in mails.Take(25))
                    {
                        //Increment retry field for expired status
                        if (mail.MailStatus == MailStatus.Expired ||
                            mail.MailStatus == MailStatus.Failed)
                            mail.RetryCounter = mail.RetryCounter + 1;

                        mail.MailStatus = MailStatus.Sending;
                        mail.ModifiedDateTime = DateTime.Now;
                        _unitOfWork.Context.Emails?.Update(mail);
                        prepareSendingEmails.Add(mail);
                    }
                    foreach (var email in prepareSendingEmails)
                    {
                        SendMail(email);
                        Thread.Sleep(200);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendMailJob Error: {ex}");
            }

            _logger.LogInfo("SendMailJob End");
        }

        #region -- Send Mail Helper --
        public void SendMail(Email emailInput)
        {
            _logger.LogInfo("Send Mail - Start");

            var email = _unitOfWork.Context.Emails?.FirstOrDefault(e =>
                            e.Id == emailInput.Id &&
                            e.MailStatus != MailStatus.Sent);

            if (email == null)
            {
                _logger.LogError($"Send Mail Error - Email query fails. - [Id={emailInput.Id}]");
                return;
            }

            try
            {
                _logger.LogInfo($"Send Mail - Email - [Id={email.Id}] - [MailStatus={email.MailStatus}]");

                // Settings for development environments
                var devEnvironment = new DeveloperEnvironment
                {
                    Enable = _developerEnv.Value.Enable,
                    DeveloperTo = _developerEnv.Value.DeveloperTo,
                    DeveloperCc = _developerEnv.Value.DeveloperCc,
                    Subject = _developerEnv.Value.Subject,
                };

                // Initialize mail instance
                var mail = new MimeMessage();

                mail.From.Add(new MailboxAddress("Eqa Systems", _smtp.Value.Email));
                mail.Sender = new MailboxAddress("Eqa Systems", _smtp.Value.Email);

                // Prepare sending email

                // MailTo
                if (!string.IsNullOrEmpty(email.To))
                {
                    if (email.To.Contains(EmailConstraints.EmailConstraintSplit))
                    {
                        var mailTos = email.To.Split(EmailConstraints.EmailConstraintSplit);
                        foreach (var mailTo in mailTos)
                        {
                            if (!string.IsNullOrEmpty(mailTo.Trim()))
                            {
                                mail.To.Add(MailboxAddress.Parse(mailTo.Trim()));
                            }
                        }
                    }
                    else
                    {
                        mail.To.Add(MailboxAddress.Parse(email.To.Trim()));
                    }
                }

                // MailCc
                if (!string.IsNullOrEmpty(email.Cc))
                {
                    if (email.Cc.Contains(EmailConstraints.EmailConstraintSplit))
                    {
                        var mailCcs = email.Cc.Split(EmailConstraints.EmailConstraintSplit);
                        foreach (var mailCc in mailCcs)
                        {
                            if (!string.IsNullOrEmpty(mailCc.Trim()))
                            {
                                mail.Cc.Add(MailboxAddress.Parse(mailCc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        mail.Cc.Add(MailboxAddress.Parse(email.Cc.Trim()));
                    }
                }

                // Mail Subject & Body
                mail.Subject = email.Subject;

                // Set Developer Environment Data 
                if (devEnvironment.Enable)
                {
                    mail.Subject = $"{devEnvironment.Subject} - {mail.Subject}";

                    if (!string.IsNullOrEmpty(devEnvironment.DeveloperTo) &&
                        !string.IsNullOrEmpty(devEnvironment.DeveloperCc))
                    {
                        // Clear old ones
                        mail.To.Clear();
                        mail.Cc.Clear();

                        // Set new data in dev env
                        mail.To.Add(MailboxAddress.Parse(devEnvironment.DeveloperTo.Trim()));
                        mail.Cc.Add(MailboxAddress.Parse(devEnvironment.DeveloperCc.Trim()));
                    }
                }

                var bodyEmail = string.IsNullOrEmpty(email.Body) ? string.Empty : email.Body;
                var body = new BodyBuilder
                {
                    HtmlBody = bodyEmail
                };
                mail.Body = body.ToMessageBody();
                var bodySize = Encoding.ASCII.GetByteCount(bodyEmail);

                // Need process attachments here

                // Limit size email to 25mb
                if (bodySize < EmailConstraints.EmailSizeLimit)
                {
                    // Start Client
                    using var smtpClient = new SmtpClient();
                    var host = _smtp.Value.Host;
                    var port = Convert.ToInt32(_smtp.Value.Port);

                    smtpClient.Connect(host, port, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate(_smtp.Value.Email, _smtp.Value.Password);
                    smtpClient.Send(mail);
                    smtpClient.Disconnect(true);
                    // End Client

                    // Update email status after sending email
                    email.MailStatus = MailStatus.Sent;
                    email.ErrorMessage = $"Email Sent - Size in bytes - {bodySize}";
                    _unitOfWork.Context.Emails?.Update(email);
                    _unitOfWork.Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("exceeded storage allocation"))
                    email.RetryCounter = 20;

                _logger.LogError($"Send Mail Error - Error while sending email - {ex}");

                email.MailStatus = MailStatus.Failed;
                email.ErrorMessage = $"Error while sending email - {ex.Message}";
                _unitOfWork.Context.Emails?.Update(email);
                _unitOfWork.Context.SaveChanges();
            }

            _logger.LogInfo($"Send Mail - End");
        }
        #endregion
    }
}
