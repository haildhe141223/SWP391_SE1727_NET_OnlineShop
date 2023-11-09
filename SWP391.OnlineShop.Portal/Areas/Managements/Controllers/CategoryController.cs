using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static ServiceStack.Diagnostics.Events;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.VoucherModels;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class CategoryController : BaseController
    {
		private readonly UserManager<User> _userManager;
		private readonly ILoggerService _logger;
		private readonly IJsonServiceClient _client;

		public CategoryController(
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
            var categories = await _client.GetAsync(new GetAllCategories());
            return View(categories);
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _client.GetAsync(new GetCategoryById()
            {
                Id = id
            });
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var api = await _client.PutAsync(new PutUpdateCategory()
            {
                Id = request.Id,
                CategoryName = request.CategoryName,
                CategoryType = request.CategoryType
            });
            if (api.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Create successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Create fail! {api.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
			var api = await _client.DeleteAsync(new DeleteCategory()
			{
				Id = id
			});
			if (api.StatusCode == Common.Enums.StatusCode.Success)
			{
				TempData["SuccessMess"] = "Delete successfully!";
				return RedirectToAction("Index");
			}
			TempData["ErrorMess"] = $"Delete fail! {api.ErrorMessage}";
			return RedirectToAction("Index");
		}

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var api = await _client.PostAsync(new PostAddCategory()
            {
                CategoryName = request.CategoryName,
                CategoryType = request.CategoryType
            });
            if (api.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Create successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Create fail! {api.ErrorMessage}";
            return View(request);
        }
    }
}
