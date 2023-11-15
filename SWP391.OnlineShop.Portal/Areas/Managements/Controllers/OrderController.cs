using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AddressModel;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class OrderController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IJsonServiceClient _client;

        public OrderController(
            UserManager<User> userManager,
            ILoggerService logger,
            IJsonServiceClient client)
        {
            _userManager = userManager;
            _logger = logger;
            _client = client;
        }
        public async Task<IActionResult> Index()
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
            var orders = await _client.GetAsync(new GetAllOrder());
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
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
            var order = await _client.GetAsync(new GetCartInfo()
            {
                Id = id
            });
            var productSlider = await _client.GetAsync(new GetAllProduct());

            order.CustomerPhone = user.PhoneNumber;

            if (string.IsNullOrEmpty(order.CustomerAddress))
            {
                var userAddress = await _client.GetAsync(new GetAddressByUser()
                {
                    Email = email
                });
                order.CustomerAddress = userAddress.FullAddress;
            }
            if (string.IsNullOrEmpty(order.CustomerEmail))
            {

                order.CustomerAddress = email;
            }
            order.Sliders = productSlider.Take(8).ToList();
            return View(order);
        }
    }
}
