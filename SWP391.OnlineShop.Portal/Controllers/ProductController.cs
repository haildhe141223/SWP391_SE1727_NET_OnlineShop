using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IJsonServiceClient _client;

        public ProductController(
            IJsonServiceClient client)
        {
            _client = client;
        }

		public async Task<IActionResult> Index(int categoryId, int page)
		{
			var latestProducts = new List<ProductViewModel>();
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

            return View(productDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(
            string name,
            string email,
            string number,
            string message,
            int productId)
        {
            await _client.PostAsync(new Comment
            {
                Email = email,
                Message = message,
                Name = name,
                Phone = number,
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
