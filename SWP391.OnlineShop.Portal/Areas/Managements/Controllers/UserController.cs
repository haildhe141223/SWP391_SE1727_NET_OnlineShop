using Microsoft.AspNetCore.Mvc;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
