using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.DashboardModels;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IJsonServiceClient _client;

        public DashboardController(
            IConfiguration config,
            IJsonServiceClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardVm = await _client.GetAsync(new GetDashboard());
            return View(dashboardVm);
        }

        [Authorize(Roles = $"{RoleConstraints.Admin}")]
        public IActionResult JobSchedule()
        {
            var hangFireService = _config["HangFireService"];

            if (string.IsNullOrEmpty(hangFireService))
            {
                return RedirectToAction("ErrorNotFound", "Account");
            }

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var redirect = $"{hangFireService}?email={email}&isPersistent=true";
            return Redirect(redirect);
        }
    }
}
