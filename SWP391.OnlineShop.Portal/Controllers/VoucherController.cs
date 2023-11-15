using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ProfileModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.VoucherModels;

namespace SWP391.OnlineShop.Portal.Controllers
{
	public class VoucherController : BaseController
	{
		private readonly UserManager<User> _userManager;
		private readonly ILoggerService _logger;
		private readonly IJsonServiceClient _client;

		public VoucherController(
			UserManager<User> userManager,
			ILoggerService logger,
			IJsonServiceClient client)
		{
			_userManager = userManager;
			_logger = logger;
			_client = client;
		}
		public async Task<IActionResult> ReceiveVoucher()
		{
			var vouchers = await _client.GetAsync(new GetAllAvailableVoucher()
			{
			});
			return View(vouchers);
		}

		public async Task<IActionResult> SaveVoucher(int id)
		{
			if (id == 0)
			{
				return StatusCode(500, "Voucher Not Found");
			}
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			if (string.IsNullOrEmpty(email))
			{
				return RedirectToAction("Login", "Account");
			}
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var saveVoucher = await _client.PostAsync(new PostAddVoucherToUser()
			{
				UserId = user.Id,
				VoucherId = id
			});
			if(saveVoucher.StatusCode == Common.Enums.StatusCode.Success) {
				var updateAmount = await _client.PutAsync(new PutUpdateVoucherAmount()
				{
					Id = id
				});
				return Ok();
			}
			return StatusCode(500, "Add Failed");
		}

		public async Task<IActionResult> GetUserVoucher(string code, int orderId)
		{
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var cartDetailOrders = await _client.GetAsync(new GetCartInfo
            {
                Id = orderId
            });
			if(cartDetailOrders.OrderVouchers.Any())
			{
				return StatusCode(500, "This order already apply a voucher");
			}
            var vouchers = await _client.GetAsync(new GetUserVouchers()
			{
				UserId = user.Id
			});
			foreach (var item in vouchers)
			{
				if(item.VoucherCode == code) {
					return Ok(item);
				}
			}
			return StatusCode(500,"Voucher Not Found OR Expired");
		}

		public async Task<IActionResult> AddVoucherToOrder(int voucherId,int orderId,decimal total)
		{
			var api = await _client.PutAsync(new PutUpdateCartToContact()
			{
				Id = orderId,
				OrderStatus = Core.Models.Enums.OrderStatus.InCartContact,
				TotalCost = total
			});
			var addVoucherOrder = await _client.PostAsync(new PostAddVoucherToOrder()
			{
				OrderId = orderId,
				VoucherId = voucherId
			});
			return Ok();
		}
	}
}
