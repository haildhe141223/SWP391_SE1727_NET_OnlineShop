using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;

public class ContactService : BaseService, IContactService
{
    private readonly ILoggerService _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public ContactService(
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

    public async Task<List<EmailContactViewModel>> Get(ContactModels.GetContacts request)
    {
        var contacts = await _unitOfWork.Context.Emails
            .Where(x=>x.Category == request.Category)
            .OrderByDescending(x=>x.CreatedDateTime).ToListAsync();
        return _mapper.Map<List<EmailContactViewModel>>(contacts);
    }

    public async Task<BaseResultModel> Post(ContactModels.PostAddContact request)
    {
        try
        {
            var email = new Email
            {
                Subject = request.Subject + "+" + request.Email,
                Body = request.Message,
                To = "admin@gmail.com",
                Category = "Contact",
                Title = request.Subject
            };

            var mailId = await _mailService.SendAsync(email);

            return new BaseResultModel
            {
                StatusCode = StatusCode.Success,
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

    public async Task<EmailContactViewModel> Get(ContactModels.GetContact request)
    {
        var contact = await _unitOfWork.Context.Emails
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        return _mapper.Map<EmailContactViewModel>(contact);
    }

}