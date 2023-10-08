using Microsoft.AspNetCore.Mvc;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
