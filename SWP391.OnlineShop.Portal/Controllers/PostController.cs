using Microsoft.AspNetCore.Mvc;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class PostController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
