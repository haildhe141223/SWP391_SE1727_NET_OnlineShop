using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static ServiceStack.Diagnostics.Events;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;

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
            var categories = await _client.GetAsync(new GetAllCategory());
            return View(categories);
        }

        public IActionResult Update(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
