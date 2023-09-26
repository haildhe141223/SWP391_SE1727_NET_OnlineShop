using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Portal.Models;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using System.Diagnostics;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class HomeController : Controller
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
            _logger.LogInfo("1. Home Index - Start");
            var products = await _client.GetAsync(new GetAllProduct ());
            return View(products);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}