using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Emails;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Requests;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.RequestModels;

namespace SWP391.OnlineShop.ServiceInterface.Services;

public class RequestService : BaseService, IRequestService
{
    private readonly ILoggerService _logger;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public RequestService(
        ILoggerService logger,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
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
        _roleManager = roleManager;
    }

    public async Task<List<RequestManageViewModel>> Get(GetRequests request)
    {
        var requests = await _unitOfWork.Context.Requests
            .Include(r => r.User)
            .Where(r => r.RequestStatus != RequestStatus.Approved &&
                        r.RequestStatus != RequestStatus.Rejected)
            .OrderBy(r => r.CreatedDateTime)
            .ToListAsync();

        return _mapper.Map<List<RequestManageViewModel>>(requests);
    }

    public async Task<List<RequestManageViewModel>> Get(GetProcessedRequests request)
    {
        var requests = await _unitOfWork.Context.Requests
            .Include(r => r.User)
            .Where(r => r.RequestStatus == RequestStatus.Approved ||
                        r.RequestStatus == RequestStatus.Rejected)
            .OrderBy(r => r.CreatedDateTime)
            .ToListAsync();

        return _mapper.Map<List<RequestManageViewModel>>(requests);
    }

    public async Task<RequestManageViewModel> Get(GetRequest request)
    {
        var requestExist = await _unitOfWork.Context.Requests
            .Include(r => r.User)
            .FirstOrDefaultAsync(x => x.Id == request.RequestId);

        return _mapper.Map<RequestManageViewModel>(requestExist ?? new Request());
    }

    public async Task<BaseResultModel> Delete(DeleteRequest request)
    {
        try
        {
            var requestExist = await _unitOfWork.Context.Requests
                .FirstOrDefaultAsync(x => x.Id == request.RequestId);

            if (requestExist == null)
            {
                throw new Exception($"Did not found any request match with id - {request.RequestId}");
            }

            _unitOfWork.Context.Remove(requestExist);
            await _unitOfWork.CompleteAsync();

            return new BaseResultModel
            {
                StatusCode = StatusCode.Success
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in DeleteRequest - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Put(PutUpdateRequest request)
    {
        try
        {
            var requestExist = await _unitOfWork.Context.Requests
                .FirstOrDefaultAsync(x => x.Id == request.RequestId);

            if (requestExist == null)
            {
                throw new Exception($"Did not found any request match with id - {request.RequestId}");
            }

            if (request.RequestStatus == RequestStatus.Approved)
            {
                var user = await _userManager.FindByIdAsync(requestExist.UserId.ToString());

                if (user != null)
                {
                    if (requestExist.RequestType == RequestType.RequestToBecomeMarketing)
                    {
                        await _userManager.AddToRoleAsync(user, RoleConstraints.Marketing);
                    }
                    else if (requestExist.RequestType == RequestType.RequestToBecomeSaleManager)
                    {
                        await _userManager.AddToRoleAsync(user, RoleConstraints.SaleManager);
                    }
                }
                else
                {
                    throw new Exception($"Did not found user with id [{requestExist.Id}]");
                }
            }

            requestExist.RequestStatus = request.RequestStatus;
            requestExist.Reply = request.RequestReplyData;

            _unitOfWork.Context.Update(requestExist);
            await _unitOfWork.CompleteAsync();

            return new BaseResultModel
            {
                StatusCode = StatusCode.Success
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in PutUpdateRequest - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Put(PutRestoreRequest request)
    {
        try
        {
            var requestExist = await _unitOfWork.Context.Requests
                .FirstOrDefaultAsync(x => x.Id == request.RequestId);

            if (requestExist == null)
            {
                throw new Exception($"Did not found any request match with id - {request.RequestId}");
            }

            requestExist.RequestStatus = request.RequestStatus;

            _unitOfWork.Context.Update(requestExist);
            await _unitOfWork.CompleteAsync();

            return new BaseResultModel
            {
                StatusCode = StatusCode.Success
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in PutRestoreRequest - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}