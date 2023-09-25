using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Portal.Models;
using System.Diagnostics;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ProductModels;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJsonServiceClient _client;

        public HomeController(ILogger<HomeController> logger, IJsonServiceClient client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index()
        {
            var products = _client.GetAsync(new GetAllProduct {
            
            });
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