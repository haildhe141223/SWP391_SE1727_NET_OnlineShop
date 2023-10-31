using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.VoucherModels;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class VoucherController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IJsonServiceClient _client;

        public VoucherController(
            UserManager<User> userManager,
            ILoggerService logger,
            IJsonServiceClient client)
        {
            _userManager = userManager;
            _logger = logger;
            _client = client;
        }
        public async Task<IActionResult> Index()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var vouchers = await _client.GetAsync(new GetAllVoucherByUser()
            {
                Email = email
            });
            return View(vouchers);
        }

        public async Task<IActionResult> Update(int id)
        {
            var voucher = await _client.GetAsync(new GetVoucherById()
            {
                Id = id
            });
            var products = await _client.GetAsync(new GetProductOfVoucher
            {
                VoucherId = voucher.Id
            });
            ViewData["ProductList"] = new SelectList(products.OrderBy(p => p.Id), "Id", "ProductName");
            return View(voucher);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VoucherViewModels request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }
            var product = await _client.GetAsync(new GetAllProduct());
            ViewData["ProductList"] = new SelectList(product.OrderBy(p => p.Id), "Id", "ProductName");
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            if (request.StartDateTime < request.EndDateTime)
            {
                TempData["ErrorMess"] = "Start Time must happen before Expired Date";
                return View(request);
            }
            else if (DateTime.Now > request.EndDateTime)
            {
                TempData["ErrorMess"] = "End Time must be greater than today";
                return View(request);
            }
            var api = await _client.PostAsync(new PutUpdateVoucher()
            {
                Amount = request.Amount,
                Email = email,
                EndDateTime = request.EndDateTime,
                StartDateTime = request.StartDateTime,
                Type = request.Type,
                Value = request.Value,
                VoucherCode = request.VoucherCode,
                VoucherName = request.VoucherName
            });
            if (api.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Update successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Create fail! {api.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var api = await _client.DeleteAsync(new DeleteVoucher()
            {
                Id = id
            });
            if (api.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Create successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Create fail! {api.ErrorMessage}";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Add()
        {
            var product = await _client.GetAsync(new GetAllProduct());
            ViewData["ProductList"] = new SelectList(product.OrderBy(p => p.Id), "Id", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(VoucherViewModels request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }
            var product = await _client.GetAsync(new GetAllProduct());
            ViewData["ProductList"] = new SelectList(product.OrderBy(p => p.Id), "Id", "ProductName");
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            if (request.StartDateTime > request.EndDateTime)
            {
                TempData["ErrorMess"] = "Start Time must happen before Expired Date";
                return View(request);
            }
            else if (DateTime.Now > request.EndDateTime)
            {
                TempData["ErrorMess"] = "End Time must be greater than today";
                return View(request);
            }
            var api = await _client.PostAsync(new PostAddVoucher()
            {
                Amount = request.Amount,
                Email = email,
                EndDateTime = request.EndDateTime,
                ProductId = request.ProductIds,
                StartDateTime = request.StartDateTime,
                Type = request.Type,
                Value = request.Value,
                VoucherCode = request.VoucherCode,
                VoucherName = request.VoucherName
            });
            if (api.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Create successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Create fail! {api.ErrorMessage}";
            return View(request);
        }

        public IActionResult AddVoucherForUser(int id)
        {
            return View();
        }

    }
}
