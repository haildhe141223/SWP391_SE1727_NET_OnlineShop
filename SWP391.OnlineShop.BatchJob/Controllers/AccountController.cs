using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.BatchJob.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, bool isPersistent)
        {
            var userTest = User;

            if (string.IsNullOrEmpty(email))
                return NotFound();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            await _signInManager.SignInAsync(user, isPersistent);

            return Redirect("/hangfire");
        }

        public IActionResult ErrorForbidden()
        {
            return Content("403 Error Forbidden");
        }
    }
}
