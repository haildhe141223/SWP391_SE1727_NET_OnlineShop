﻿using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Addresses;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AddressModel;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AddressService(UserManager<User> userManager,
            ILoggerService logger,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddressViewModel> Get(GetAddressByUser request)
        {
            var result = new AddressViewModel();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    var address = _unitOfWork.Context.Addresses
                        .FirstOrDefault(a => a.UserId == user.Id && a.IsDefault == true);

                    if (address != null)
                    {
                        result.Id = address.Id;
                        result.FullAddress = address.FullAddress;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAddressByUser Error: {ex.Message}");
            }
            return result;
        }
        public List<DistrictViewModel> Get(GetDistrictByProvince request)
        {
            var result = new List<DistrictViewModel>();
            try
            {
                var districts = _unitOfWork.Context.Districts
                    .Where(d => d.ProvinceId == request.ProvinceId)
                    .ToList();

                if (districts.Any())
                {
                    foreach (var d in districts)
                    {
                        var address = new DistrictViewModel
                        {
                            DistrictName = d.DistrictName,
                            Id = d.Id
                        };
                        result.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetDistrictByProvince Err: {ex.Message}");
            }
            return result;
        }

        public List<ProvinceViewModel> Get(GetAllProvince request)
        {
            var result = new List<ProvinceViewModel>();

            try
            {
                var provinces = _unitOfWork.Context.Provinces.ToList();
                if (provinces.Any())
                {
                    foreach (var p in provinces)
                    {
                        var address = new ProvinceViewModel
                        {
                            Id = p.Id,
                            ProvinceName = p.ProvinceName
                        };
                        result.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllProvince Err: {ex.Message}");
            }
            return result;
        }

        public List<WardViewModel> Get(GetWardByDistrict request)
        {
            var result = new List<WardViewModel>();
            try
            {
                var wards = _unitOfWork.Context.Wards.Where(d => d.DistrictId == request.DistrictId).ToList();
                if (wards.Any())
                {
                    foreach (var w in wards)
                    {
                        var address = new WardViewModel
                        {
                            WardName = w.WardName,
                            Id = w.Id
                        };
                        result.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetDistrictByProvince Err: {ex.Message}");
            }
            return result;
        }

        public async Task<BaseResultModel> Put(PutUpdateAddress request)
        {
            var result = new BaseResultModel();
            try
            {
                var address = _unitOfWork.Context.Addresses.FirstOrDefault(a => a.Id == request.Id);
                if (address != null)
                {
                    address.FullAddress = request.FullAddress;
                    var row = await _unitOfWork.CompleteAsync();
                    if (row > 0)
                    {
                        result.StatusCode = Common.Enums.StatusCode.Success;
                        return result;
                    }
                    result.StatusCode = Common.Enums.StatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutUpdateAddress Error: {ex.Message}");
            }
            return result;
        }
    }
}
