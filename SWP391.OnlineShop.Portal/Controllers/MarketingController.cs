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

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel request)
        {
            await _client.PostAsync(new PostAddProduct
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Thumbnail = request.Thumbnail,
                CategoryId = request.CategoryId,
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditProduct(int id)
        {

            var product = await _client.GetAsync(new GetProductById
            {
                ProductId = id
            });
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel request)
        {
            await _client.PutAsync(new PutUpdateProduct
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Thumbnail = request.Thumbnail,
                CategoryId = request.CategoryId
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _client.DeleteAsync(new DeleteProduct
            {
                ProductId = id
            });
            return RedirectToAction("Index");
        }

    }
}
