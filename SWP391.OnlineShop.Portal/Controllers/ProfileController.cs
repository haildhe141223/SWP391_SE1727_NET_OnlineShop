using Microsoft.AspNetCore.Mvc;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class ProfileController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
