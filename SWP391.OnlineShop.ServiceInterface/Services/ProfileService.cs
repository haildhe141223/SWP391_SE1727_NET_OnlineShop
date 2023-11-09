using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Emails;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ProfileModels;

namespace SWP391.OnlineShop.ServiceInterface.Services;

public class ProfileService : BaseService, IProfileService
{
    private readonly ILoggerService _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public ProfileService(
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

    public async Task<BaseResultModel> Put(PutUpdateUserName request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                user.UserName = request.NewName;
                await _userManager.UpdateAsync(user);

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("User does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PutUpdateUserName request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Put(PutUpdateUserEmail request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                user.Email = request.NewEmail;
                await _userManager.UpdateAsync(user);

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("User does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PutUpdateUserEmail request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Put(PutUpdateUserAvatar request)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user != null)
            {
                user.Image = request.NewAvatar;
                await _userManager.UpdateAsync(user);

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("User does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PutUpdateUserEmail request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}