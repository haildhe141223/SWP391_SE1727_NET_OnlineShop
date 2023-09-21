using Microsoft.AspNetCore.Mvc;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
