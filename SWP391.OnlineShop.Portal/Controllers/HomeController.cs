using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Portal.Models;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System.Diagnostics;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IJsonServiceClient _client;
        private readonly ILoggerService _logger;

        public HomeController(
            IJsonServiceClient client,
        ILoggerService logger)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            //Get all products
            var latestProducts = await _client.GetAsync(new GetAllProduct());

            //Get hot deal product
            var hotDealProduct = await _client.GetAsync(new GetHotDealProduct());

            //Get deal product of week
            var dealProductOfWeeks = await _client.GetAsync(new GetDealProductOfWeek());

            //Get all categories
            var categories = await _client.GetAsync(new GetAllCategory());

            var products = new HomeViewModels
            {
                LatestProducts = latestProducts,
                HotDealProduct = hotDealProduct,
                ProductsOfWeek = dealProductOfWeeks,
                Categories = categories
            };

            return View(products);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var data = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            _logger.LogError($"Error - {data.RequestId}");
            return StatusCode(500, "500: Error with request id. An error occurred while processing your request.");
        }
    }
}