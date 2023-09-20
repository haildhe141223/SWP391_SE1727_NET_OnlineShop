using Microsoft.AspNetCore.Mvc;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
