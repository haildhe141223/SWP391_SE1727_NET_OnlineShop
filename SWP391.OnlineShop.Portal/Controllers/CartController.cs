using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.Portal.Controllers
{
	public class CartController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly IJsonServiceClient _client;
		private readonly ILoggerService _logger;
		private readonly IConfiguration _config;
		private readonly UserManager<User> _userManager;

		public CartController(
		   SignInManager<User> signInManager,
		   IJsonServiceClient client,
		   ILoggerService logger,
		   IConfiguration config,
		   UserManager<User> userManager)
		{
			_signInManager = signInManager;
			_client = client;
			_logger = logger;
			_userManager = userManager;
			_config = config;
		}

		public async Task<IActionResult> Index()
		{
			var email = "";/* User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if(string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }*/
			var cartDetailOrders = await _client.GetAsync(new GetCartDetailByUser
			{
				Email = email
			});
			var cartContactOrders = await _client.GetAsync(new GetCartContactByUser
			{
				Email = email
			});
			var cartCompleteOrders = await _client.GetAsync(new GetCartCompletionByUser
			{
				Email = email
			});
			if (cartCompleteOrders.OrderDetails?.Count <= 0 && cartDetailOrders.OrderDetails?.Count <= 0 && cartContactOrders.OrderDetails?.Count <= 0)
			{
				return View(cartDetailOrders);
			}
			else
			{
				if (cartDetailOrders.OrderDetails?.Count <= 0)
				{
					if (cartContactOrders.OrderDetails?.Count <= 0)
					{
						return RedirectToAction("Checkout", "Cart");
					}
					return View(cartContactOrders);
				}
				return View(cartDetailOrders);

			}
		}

		public async Task<IActionResult> Checkout()
		{
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			var cartCompleteOrders = await _client.GetAsync(new GetCartCompletionByUser
			{
				Email = email
			});
			return View(cartCompleteOrders);
		}

		public IActionResult Confirmation()
		{
			return View();
		}
	}
}
