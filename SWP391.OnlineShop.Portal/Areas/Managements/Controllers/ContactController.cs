using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ContactModels;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class ContactController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILoggerService _logger;
        private readonly IJsonServiceClient _client;

        public ContactController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILoggerService logger,
            IJsonServiceClient client)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _client.GetAsync(new GetContacts
            {
                Category = "Contact"
            });
            return View(contacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contactDetail = await _client.GetAsync(new GetContact
            {
                Id = id,
            });
            return View(contactDetail);
        }
    }
}
