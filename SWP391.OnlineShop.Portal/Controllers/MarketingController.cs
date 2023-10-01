using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class MarketingController : Controller
    {
        private readonly IJsonServiceClient _client;
        private readonly ILoggerService _logger;

        public MarketingController(
            IJsonServiceClient client,
        ILoggerService logger)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInfo("1. Home Index - Start");

            //Get all products
            var latestProducts = await _client.GetAsync(new GetAllProduct());

            return View(latestProducts);
        }
        
    }
}
