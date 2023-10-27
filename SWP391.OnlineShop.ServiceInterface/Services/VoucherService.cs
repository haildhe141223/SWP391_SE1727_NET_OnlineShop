using AutoMapper;
using Microsoft.AspNetCore.Identity;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.VoucherModels;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
	public class VoucherService : BaseService, IVoucherService
	{
		private readonly ILoggerService _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IUnitOfWork _unitOfWork;
		public VoucherService(UserManager<User> userManager,
			ILoggerService logger,
			IMapper mapper,
			RoleManager<Role> roleManager,
			SignInManager<User> signInManager,
			IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_logger = logger;
			_mapper = mapper;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_unitOfWork = unitOfWork;
		}
		public async Task<BaseResultModel> Delete(DeleteVoucher request)
		{
			var result = new BaseResultModel();
			try
			{
				var voucher = _unitOfWork.Vouchers.GetById(request.Id);
				if (voucher == null)
				{
					result.StatusCode = Common.Enums.StatusCode.NotFound;
					result.ErrorMessage = "Voucher doesn't exist";
					return result;
				}
				_unitOfWork.Vouchers.Delete(voucher.Id);
				int rows = await _unitOfWork.CompleteAsync();
				if (rows > 0)
				{
					result.StatusCode = Common.Enums.StatusCode.Success;
					return result;
				}
				result.StatusCode = Common.Enums.StatusCode.InternalServerError;
				result.ErrorMessage = "Delete Failed";
			}
			catch (Exception ex)
			{
				_logger.LogError($"DeleteVoucher error: {ex}");
			}
			return result;
		}

		public async Task<List<VoucherViewModels>> Get(GetAllVoucherByUser request)
		{
			var result = new List<VoucherViewModels>();
			try
			{
				var user = await _userManager.FindByEmailAsync(request.Email);
				if(user == null)
				{
					throw new Exception("User Not Found");
				}
				var vouchers = _unitOfWork.Vouchers.GetVouchersCreatedUser(user.Id);
				foreach (var voucher in vouchers)
				{
					if(voucher != null)
					{
						var voucherVM = _mapper.Map<VoucherViewModels>(voucher);
						result.Add(voucherVM);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllVoucherByUser error: {ex}");
			}
			return result;
		}

		public VoucherViewModels Get(GetVoucherById request)
		{
			var result = new VoucherViewModels();
			try
			{
				var voucher = _unitOfWork.Vouchers.GetVoucherInfo(request.Id);
				if (voucher == null)
				{
					result.StatusCode = Common.Enums.StatusCode.NotFound;
					result.ErrorMessage = "Voucher doesn't exist";
					return result;
				}
				result = _mapper.Map<VoucherViewModels>(voucher);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetVoucherById error: {ex}");
			}
			return result;
		}

		public async Task<BaseResultModel> Post(PostAddVoucher request)
		{
			var result = new BaseResultModel();
			try
			{
				var user = await _userManager.FindByEmailAsync(request.Email);
				if (user == null)
				{
					result.StatusCode = Common.Enums.StatusCode.BadRequest;
					result.ErrorMessage = "User doesn't exist";
					return result;
				}
				var voucher = new Voucher()
				{
					Amount = request.Amount,
					CreatedBy = user.Id,
					EndDateTime = request.EndDateTime,
					StartDateTime = request.StartDateTime,
					Status = Core.Models.Enums.Status.Active,
					VoucherCode = request.VoucherCode,
					VoucherName = request.VoucherName,
					Type = request.Type,
					Value = request.Value,
				};
				_unitOfWork.Vouchers.Add(voucher);
				int rows = await _unitOfWork.CompleteAsync();
				if (rows > 0)
				{
					var productVoucher = new ProductVoucher()
					{
						ProductId = request.ProductId,
						VoucherId = voucher.Id
					};
					_unitOfWork.Context.ProductVouchers.Add(productVoucher);
                    await _unitOfWork.CompleteAsync();
                    result.StatusCode = Common.Enums.StatusCode.Success;
					return result;
				}
				result.StatusCode = Common.Enums.StatusCode.InternalServerError;
				result.ErrorMessage = "Error";
			}
			catch (Exception ex)
			{
				_logger.LogError($"PostAddVoucher error: {ex}");
			}
			return result;
		}

		public async Task<BaseResultModel> Put(PutUpdateVoucher request)
		{
			var result = new BaseResultModel();
			try
			{
				var voucher = _unitOfWork.Vouchers.GetById(request.Id);
				if (voucher == null)
				{
					result.StatusCode = Common.Enums.StatusCode.NotFound;
					result.ErrorMessage = "Voucher doesn't exist";
					return result;
				}
				voucher.VoucherName = request.VoucherName;
				voucher.VoucherCode = request.VoucherCode;
				voucher.Amount = request.Amount;
				voucher.StartDateTime = request.StartDateTime;
				voucher.EndDateTime = request.EndDateTime;
				voucher.Type = request.Type;
				voucher.Value = request.Value;
				_unitOfWork.Vouchers.Update(voucher);
				int rows = await _unitOfWork.CompleteAsync();
				if (rows > 0)
				{
					result.StatusCode = Common.Enums.StatusCode.Success;
					return result;
				}
				result.StatusCode = Common.Enums.StatusCode.InternalServerError;
				result.ErrorMessage = "Error";
			}
			catch (Exception ex)
			{
				_logger.LogError($"PutUpdateVoucher error: {ex}");
			}
			return result;
		}
	}
}
