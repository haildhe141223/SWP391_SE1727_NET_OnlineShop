using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IJsonServiceClient _client;
        private readonly ILoggerService _logger;

        public ProductController(
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

            //Get all categories
            var categories = await _client.GetAsync(new GetAllCategory());

            //Get deal product of week
            var dealProductOfWeeks = await _client.GetAsync(new GetDealProductOfWeek());

            var productCategories = new ProductCategoryViewModel
            {
                LatestProducts = latestProducts,
                Categories = categories,
                ProductsOfWeek = dealProductOfWeeks
            };
            return View(productCategories);
        }

        public async Task<IActionResult> Details(int id)
        {
            //Get product detail
            var product = await _client.GetAsync(new GetProductById
            {
                ProductId = id
            });

            //Get deal product of week
            var dealProductOfWeeks = await _client.GetAsync(new GetDealProductOfWeek());

            var productDetail = new HomeViewModel
            {
                ProductDetail = product,
                ProductsOfWeek = dealProductOfWeeks
            };

            return View(productDetail);
        }

        public IActionResult Tracking()
        {
            return View();
        }
    }
}
