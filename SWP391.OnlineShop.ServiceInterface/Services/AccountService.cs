using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Common.Templates;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Emails;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AccountModels;

namespace SWP391.OnlineShop.ServiceInterface.Services;

public class AccountService : BaseService, IAccountService
{
    private readonly ILoggerService _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public AccountService(
        ILoggerService logger,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMailService mailService)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mailService = mailService;
    }

    public async Task<List<UserViewModel>> Get(GetUsers request)
    {
        // Descending
        if (request.IsDesc)
        {
            var users = await _userManager.Users.OrderByDescending(x => x.Id).Take(request.Size).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
        // Ascending
        else
        {
            var users = await _userManager.Users.Take(request.Size).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
    }

    public async Task<UserViewModel> Get(GetUser request)
    {
        var users = await _userManager.FindByIdAsync(request.Id);
        return _mapper.Map<UserViewModel>(users);
    }

    public async Task<BaseResultModel> Get(GetExternalUser request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"User with email [{request.Email}] does not exist.");
            }

            var externalLoginInfo = await _unitOfWork.Context.UserLogins.FirstOrDefaultAsync(x =>
                x.ProviderKey == request.ProviderKey && x.UserId == user.Id);

            if (externalLoginInfo == null)
            {
                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception($"User external info with email [{request.Email}] is exist.");
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in GetUser request - {e}");
            return new BaseResultModel
            {
                ErrorMessage = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Get(GetLogin request)
    {
        try
        {
            var userExist = await _userManager.FindByEmailAsync(request.Email);

            if (userExist == null)
                throw new Exception(
                    $"This email {request.Email} does not exist. Please contact admin");

            return new BaseResultModel { StatusCode = StatusCode.Success };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in GetLogin request: {e}");
            return new BaseResultModel
            {
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Get(GetExternalLogin request)
    {
        var providerDisplayName = request.ProviderDisplayName;
        try
        {
            var externalEmail = request.ExternalEmail;
            var externalUsername = request.Username;

            if (externalEmail == null || externalUsername == null)
                throw new Exception($"Found some problem while login with {providerDisplayName}");

            var isExternalLogin = await _signInManager.ExternalLoginSignInAsync(request.LoginProvider,
                request.ProviderKey, false);

            // Check user login success or not
            if (!isExternalLogin.Succeeded || isExternalLogin.IsNotAllowed)
                return new BaseResultModel
                {
                    StatusCode = StatusCode.Forbidden,
                    ErrorMessage = $"Login fail. Found some problems while logging in with {providerDisplayName}. "
                                   + "You might enter wrong password"
                };

            if (isExternalLogin.IsLockedOut) throw new Exception("This account already locked. Please contact admin");

            var userExist = await _userManager.FindByEmailAsync(request.ExternalEmail);

            if (userExist == null)
                throw new Exception(
                    $"This account {providerDisplayName} does not exist. Please contact admin");

            return new BaseResultModel { StatusCode = StatusCode.Success };

        }
        catch (Exception e)
        {
            _logger.LogError($"Error in GetExternalLogin request: {e}");
            return new BaseResultModel
            {
                StatusCode = StatusCode.InternalServerError,
                ErrorMessage = $"Login fail. Found some problems while logging in with {providerDisplayName}. " +
                               "Please contact admin."
            };
        }
    }

    public async Task<BaseResultModel> Post(PostRegisterAccount request)
    {
        try
        {
            var user = new User
            {
                Email = request.RegisterViewModel.Email,
                UserName = request.RegisterViewModel.Username.ToTitleCase(),
                Image = "https://i.ibb.co/NK98Q7p/Image.png"
            };

            var isCreatedUser = await _userManager.CreateAsync(user, request.RegisterViewModel.Password);

            if (isCreatedUser.Succeeded)
            {
                var userExist = await _userManager.FindByEmailAsync(request.RegisterViewModel.Email);
                if (userExist != null)
                {
                    await _userManager.AddToRoleAsync(userExist, RoleConstraints.Customer);

                    return new BaseResultModel
                    {
                        StatusCode = StatusCode.Success,
                        SuccessMessage = "Created user success. Please double-check again."
                    };
                }

                throw new Exception($"Create user fail. Cannot find user with email [{request.RegisterViewModel.Email}]");
            }

            throw new Exception($"Create user fail. {string.Join(", ", isCreatedUser.Errors.Select(x => x.Description).ToList())}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in PostRegisterAccount request - error: {ex}");

            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Post(PostConfirmRegisterEmail request)
    {
        try
        {
            if (request.Category == EmailConstraints.EmailNotificationCategory)
            {
                var userEmail = request.To;
                var userName = request.To.Split('@')[0];

                var confirmEmailSubject = EmailTemplates.RegisterEmailConfirmSubject;
                var confirmEmailBody = EmailTemplates.RegisterEmailConfirmTemplate;

                if (!string.IsNullOrEmpty(confirmEmailBody))
                {
                    confirmEmailBody = confirmEmailBody
                        .Replace("{{Username}}", userName)
                        .Replace("{{UserEmail}}", userEmail)
                        .Replace("{{ConfirmRegisterLink}}", request.LinkConfirmPassword);
                }

                var email = new Email
                {
                    Subject = confirmEmailSubject,
                    Body = confirmEmailBody,
                    To = userEmail,
                    Category = request.Category,
                    Title = confirmEmailSubject
                };

                var mailId = await _mailService.SendAsync(email);
                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success,
                    SuccessMessage = $"Send mail success with id [{mailId}]"
                };
            }

            if (request.Category == EmailConstraints.EmailNotificationWithPasswordCategory)
            {
                var userEmail = request.To;
                var userName = request.To.Split('@')[0];

                var confirmEmailSubject = EmailTemplates.RegisterEmailConfirmSubject;
                var confirmEmailBody = EmailTemplates.RegisterEmailConfirmWithPasswordTemplate;

                if (!string.IsNullOrEmpty(confirmEmailBody))
                {
                    confirmEmailBody = confirmEmailBody
                        .Replace("{{Username}}", userName)
                        .Replace("{{UserEmail}}", userEmail)
                        .Replace("{{DefaultPassword}}", LoginKeyConstraints.DefaultPassword)
                        .Replace("{{ConfirmRegisterLink}}", request.LinkConfirmPassword);
                }

                var email = new Email
                {
                    Subject = confirmEmailSubject,
                    Body = confirmEmailBody,
                    To = userEmail,
                    Category = request.Category,
                    Title = confirmEmailSubject
                };

                var mailId = await _mailService.SendAsync(email);
                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success,
                    SuccessMessage = $"Send mail success with id [{mailId}]"
                };
            }

            throw new Exception("Send mail notification fail, please contact admin for double-check system.");
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in PostResetPassword request - {e}");
            return new BaseResultModel
            {
                StatusCode = StatusCode.InternalServerError,
                ErrorMessage = e.Message
            };
        }
    }

    public async Task<BaseResultModel> Post(PostConfirmChangePasswordEmail request)
    {
        try
        {
            var userEmail = request.To;
            var userName = request.To.Split('@')[0];

            var confirmEmailSubject = EmailTemplates.ForgotPasswordSubject;
            var confirmEmailBody = EmailTemplates.ForgotPasswordTemplate;

            if (!string.IsNullOrEmpty(confirmEmailBody))
            {
                confirmEmailBody = confirmEmailBody
                    .Replace("{{Username}}", userName)
                    .Replace("{{UserEmail}}", userEmail)
                    .Replace("{{ResetPassLink}}", request.LinkResetPassword);
            }

            var email = new Email
            {
                Subject = confirmEmailSubject,
                Body = confirmEmailBody,
                To = userEmail,
                Category = request.Category,
                Title = confirmEmailSubject
            };

            var mailId = await _mailService.SendAsync(email);

            return new BaseResultModel
            {
                StatusCode = StatusCode.Created,
                SuccessMessage = $"Send mail success with id [{mailId}]"
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in PostResetPassword request - {e}");
            return new BaseResultModel
            {
                StatusCode = StatusCode.InternalServerError,
                ErrorMessage = e.Message
            };
        }
    }


    public async Task<List<UserViewModel>> Get(GetCustomers request)
    {
        // Descending
        if (request.IsDesc)
        {
            var customers = await _userManager.GetUsersInRoleAsync(RoleConstraints.Customer);
            customers = customers.OrderByDescending(x => x.Id).ToList();
            return _mapper.Map<List<UserViewModel>>(customers);
        }
        // Ascending
        else
        {
            var customers = await _userManager.GetUsersInRoleAsync(RoleConstraints.Customer);
            customers = customers.ToList();
            return _mapper.Map<List<UserViewModel>>(customers);
        }
    }

    public async Task<BaseResultModel> Put(UpdateCustomer request)
    {
        try
        {
            var customer = await _userManager.FindByIdAsync(Convert.ToString(request.Id));
            if (customer != null)
            {
                customer.LockoutEnabled = request.LockoutEnabled;
                await _userManager.UpdateAsync(customer);
            }

            return new BaseResultModel
            {
                StatusCode = StatusCode.Success,
                SuccessMessage = "Update status success"
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in UpdateCustomer request - {e}");
            return new BaseResultModel
            {
                StatusCode = StatusCode.InternalServerError,
                ErrorMessage = e.Message
            };
        }

    }

    public async Task<BaseResultModel> Delete(DeleteUser request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
            }

            return new BaseResultModel
            {
                StatusCode = StatusCode.Success
            };
        }
        catch (Exception ex)
        {
            return new BaseResultModel
            {
                ErrorMessage = $"DeleteUser request - {ex}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}