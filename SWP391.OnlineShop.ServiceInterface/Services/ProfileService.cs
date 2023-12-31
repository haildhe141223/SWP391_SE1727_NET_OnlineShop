﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Emails;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;
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

    public async Task<BaseResultModel> Delete(DeleteUserAddress request)
    {
        try
        {
            var address = await _unitOfWork.Context.Addresses
                .FirstOrDefaultAsync(x => x.Id == request.AddressId);
            if (address != null)
            {
                _unitOfWork.Context.Remove(address);
                await _unitOfWork.CompleteAsync();

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("Address does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"DeleteUserAddress request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<List<AddressViewModel>> Get(GetUserAddresses request)
    {
        var user = await _userManager.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user != null)
            return _mapper.Map<List<AddressViewModel>>(user.Addresses.OrderByDescending(x => x.IsDefault).ToList());

        return new List<AddressViewModel>();
    }

    public async Task<List<RequestDataViewModel>> Get(GetUserRequests request)
    {
        var requests = await _unitOfWork.Context.Requests
            .Where(r => r.UserId == request.UserId)
            .ToListAsync();

        return _mapper.Map<List<RequestDataViewModel>>(requests);
    }

    public List<UserVoucherViewModel> Get(GetUserVouchers request)
    {
        var vouchers = _unitOfWork.Vouchers.GetAvailableVouchersOfUser(request.UserId);
        return _mapper.Map<List<UserVoucherViewModel>>(vouchers);
    }

    public async Task<BaseResultModel> Post(PostAddUserAddress request)
    {
        try
        {
            var user = await _userManager.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user != null)
            {
                if (request.IsDefault)
                {
                    var addresses = user.Addresses;
                    foreach (var addressExist in addresses)
                    {
                        addressExist.IsDefault = false;
                    }

                    _unitOfWork.Context.UpdateRange(addresses);
                    await _unitOfWork.CompleteAsync();
                }

                var address = new Address
                {
                    IsDefault = request.IsDefault,
                    FullAddress = request.Address,
                    UserId = user.Id,
                    Status = Core.Models.Enums.Status.Active,
                    PhoneNumber = request.PhoneNumber,
                    FullName = request.FullName
                };

                await _unitOfWork.Context.AddAsync(address);
                await _unitOfWork.CompleteAsync();

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("User does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PostAddUserAddress request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Post(PostBecomeRequestMarketer request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                var viewModel = new RequestMarketingViewModel
                {
                    Author = request.Author,
                    Email = request.Email,
                    Name = request.FullName,
                    Phone = request.PhoneNumber,
                    SamplePostLink = request.SamplePostLink
                };

                var data = JsonConvert.SerializeObject(viewModel);

                var requestData = new Request
                {
                    RequestStatus = Core.Models.Enums.RequestStatus.Submitted,
                    RequestType = Core.Models.Enums.RequestType.RequestToBecomeMarketing,
                    UserId = user.Id,
                    RequestData = data
                };

                await _unitOfWork.Context.AddAsync(requestData);
                await _unitOfWork.CompleteAsync();

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("User does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PostBecomeRequestMarketer request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Post(PostBecomeRequestSaleManager request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                var viewModel = new RequestSaleManagerViewModel
                {
                    Email = request.Email,
                    Name = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    FullAddress = request.FullAddress,
                    BusinessRegistrationCertificateImage = request.BusinessCertificateLink,
                    FrontOfIdentityCardImage = request.FrontOfIdentityCardLink,
                    BackOfIdentityCardImage = request.BackOfIdentityCardLink,
                    Reason = request.Reason,
                };

                var data = JsonConvert.SerializeObject(viewModel);

                var requestData = new Request
                {
                    RequestStatus = Core.Models.Enums.RequestStatus.Submitted,
                    RequestType = Core.Models.Enums.RequestType.RequestToBecomeSaleManager,
                    UserId = user.Id,
                    RequestData = data
                };

                await _unitOfWork.Context.AddAsync(requestData);
                await _unitOfWork.CompleteAsync();

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("User does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PostBecomeRequestSaleManager request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Put(PutUpdateUserAddress request)
    {
        try
        {
            var isValidAddressId = int.TryParse(request.AddressId, out var id);
            if (!isValidAddressId)
            {
                throw new Exception("Invalid address id - Please re-check");
            }

            if (request.IsDefault)
            {
                var user = await _userManager.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (user != null)
                {
                    var addresses = user.Addresses;
                    foreach (var addressExist in addresses)
                    {
                        addressExist.IsDefault = false;
                    }

                    _unitOfWork.Context.UpdateRange(addresses);
                    await _unitOfWork.CompleteAsync();
                }
            }

            var address = await _unitOfWork.Context.Addresses
                .FirstOrDefaultAsync(x => x.Id == id);
            if (address != null)
            {
                address.FullAddress = request.Address;
                address.PhoneNumber = request.PhoneNumber;
                address.FullName = request.FullName;
                address.IsDefault = request.IsDefault;

                _unitOfWork.Context.Update(address);
                await _unitOfWork.CompleteAsync();

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("Address does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PutUpdateUserAddress request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
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

    public async Task<BaseResultModel> Put(PutUpdateUserPhone request)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user != null)
            {
                user.PhoneNumber = request.NewPhone;
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
            _logger.LogError($"PutUpdateUserPhone request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Put(PutUpdateUserGender request)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user != null)
            {
                user.Gender = request.NewGender;
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
            _logger.LogError($"PutUpdateUserGender request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResultModel> Put(PutUpdateDefaultAddress request)
    {
        try
        {
            var user = await _userManager.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user != null)
            {
                var addresses = user.Addresses;
                foreach (var addressExist in addresses)
                {
                    addressExist.IsDefault = false;
                }

                _unitOfWork.Context.UpdateRange(addresses);
                await _unitOfWork.CompleteAsync();


                var address = addresses.FirstOrDefault(x => x.Id == request.AddressId);
                if (address != null)
                {
                    address.IsDefault = true;
                    _unitOfWork.Context.Update(address);
                    await _unitOfWork.CompleteAsync();
                }

                return new BaseResultModel
                {
                    StatusCode = StatusCode.Success
                };
            }

            throw new Exception("User does not exist. Please re-check");
        }
        catch (Exception ex)
        {
            _logger.LogError($"PutUpdateDefaultAddress request - {ex}");
            return new BaseResultModel
            {
                ErrorMessage = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}