using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System.Security.Claims;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IJsonServiceClient _client;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;
        public ProductController(IJsonServiceClient client,
           ILoggerService logger,
           UserManager<User> userManager)
        {
            _client = client;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int categoryId, int page = 1)
        {
            List<ProductViewModel> latestProducts;
            if (categoryId == 0)
            {
                //Get all products
                latestProducts = await _client.GetAsync(new GetAllProduct());
            }
            else
            {
                //Get all products
                latestProducts = await _client.GetAsync(new GetProductByCategoryId
                {
                    CategoryId = categoryId
                });
            }

            ViewBag.Pages = latestProducts.Count / 9 + 1;
            ViewBag.CurrentPage = page;

            //Get all categories
            var categories = await _client.GetAsync(new GetAllCategory());

            //Get deal product of week
            var dealProductOfWeeks = await _client.GetAsync(new GetDealProductOfWeek());

            var productCategories = new ProductCategoryViewModel
            {
                LatestProducts = latestProducts.Skip((page - 1) * 9).Take(9).ToList(),
                Categories = categories,
                ProductsOfWeek = dealProductOfWeeks
            };
            return View(productCategories);
        }

        public async Task<IActionResult> Details(int id)
        {
            //Get product detail
            var product = await _client.GetAsync(new GetProductFeedbackById
            {
                ProductId = id
            });

            //Get deal product of week
            var dealProductOfWeeks = await _client.GetAsync(new GetDealProductOfWeek());

            var productDetail = new HomeViewModels
            {
                ProductDetail = product,
                ProductsOfWeek = dealProductOfWeeks
            };

            var sizeList = product.ProductSizes.Select
            (x => new SelectListItem { Value = Convert.ToString(x.Size.SizeId), Text = x.Size.SizeType }).ToList();
            ViewBag.SizeLists = sizeList;

            return View(productDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(
            string name,
            string email,
            string number,
            double point,
            string message,
            int productId)
        {

            await _client.PostAsync(new Comment
            {
                Email = email,
                Message = message,
                Name = name,
                Phone = number,
                Point = point,
                ProductID = productId
            });
            return RedirectToAction("Details", new { id = productId });
        }

        public IActionResult Tracking()
        {
            return View();
        }
    }
}
