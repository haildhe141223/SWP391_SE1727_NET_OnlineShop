using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Portal.Models;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Contacts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System.Diagnostics;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ContactModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.SliderModel;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IJsonServiceClient _client;
        private readonly ILoggerService _logger;

        public HomeController(
            IJsonServiceClient client,
            ILoggerService logger)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            //Get all active products
            var latestProducts = await _client.GetAsync(new GetAllActiveProduct());

            //Get all incoming products
            var comingProducts = await _client.GetAsync(new GetAllComingProduct());

            //Get hot deal product
            var hotDealProduct = await _client.GetAsync(new GetHotDealProduct());

            //Get deal product of week
            var dealProductOfWeeks = await _client.GetAsync(new GetDealProductOfWeek());

            //Get all sliders
            var sliders = await _client.GetAsync(new GetAllSlider());

            var products = new HomeViewModels
            {
                IncomingProduct = comingProducts
                LatestProducts = latestProducts ?? new List<ProductViewModel>(),
                HotDealProduct = hotDealProduct ?? new List<ProductViewModel>(),
                ProductsOfWeek = dealProductOfWeeks ?? new List<ProductViewModel>(),
                Sliders = sliders
            };

            return View(products);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel request)
        {
            //TODO: HaiLD update message return here
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", ", errors)}";
                _logger.LogError($"Login Error - Model Invalid: {modelError}");
                return StatusCode(500, "Please enter email or password.");
            }

            var addContact = await _client.PostAsync(new PostAddContact
            {
                Subject = request.Subject,
                Message = request.Message,
                Email = request.Email,
                Name = request.Name
            });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var data = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            _logger.LogError($"Error - {data.RequestId}");
            return StatusCode(500, "500: Error with request id. An error occurred while processing your request.");
        }
    }
}