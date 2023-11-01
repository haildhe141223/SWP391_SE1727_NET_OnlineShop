using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    [Authorize(Roles = RoleConstraints.Admin)]
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IJsonServiceClient _client;

        public UserController(
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
            var userCount = _userManager.Users.Count();

            var users = await _client.GetAsync(new AccountModels.GetUsers
            {
                Size = userCount
            });

            return View(users);
        }

        public IActionResult Details(int id)
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

        [HttpPost]
        public IActionResult Add(AddUserViewModel request)
        {
            return View();
        }
    }
}
