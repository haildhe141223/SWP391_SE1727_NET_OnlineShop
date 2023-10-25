using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceInterface.Emails
{
    public class MailService : IMailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Send(Email email)
        {
            email.MailStatus = email.MailStatus == MailStatus.ConditionalPending
                ? MailStatus.ConditionalPending
                : MailStatus.New;

            email.ModifiedDateTime = DateTime.Now;

            if (email.Id > 0)
            {
                _unitOfWork.Emails.Update(email);
                _unitOfWork.Complete();
            }
            else
            {
                _unitOfWork.Emails.Add(email);
                _unitOfWork.Complete();
            }

            return email.Id;
        }

        public async Task<int> SendAsync(Email email)
        {
            email.MailStatus = email.MailStatus == MailStatus.ConditionalPending
                ? MailStatus.ConditionalPending
                : MailStatus.New;

            email.ModifiedDateTime = DateTime.Now;

            if (email.Id > 0)
            {
                _unitOfWork.Emails.Update(email);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                await _unitOfWork.Emails.AddAsync(email);
                await _unitOfWork.CompleteAsync();
            }

            return email.Id;
        }
    }
}
