using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AccountModels;

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

            var users = await _client.GetAsync(new GetUsers
            {
                Size = userCount
            });

            users = users.Where(x => x.LockoutEnabled == false).ToList();

            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _client.GetAsync(new GetUser
            {
                Id = id.ToString()
            });


            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var isDeleteAccount = await _client.DeleteAsync(new DeleteUser
            {
                UserId = user.Id
            });

            if (isDeleteAccount.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = $"{isDeleteAccount.ErrorMessage}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            return RedirectToAction(nameof(Index));
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
