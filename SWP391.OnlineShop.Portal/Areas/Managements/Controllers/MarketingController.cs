using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.FeedbackModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.SliderModel;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    public class MarketingController : BaseController
    {
        private readonly IJsonServiceClient _client;

        public MarketingController(
            IJsonServiceClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            //Get all products
            var latestProducts = await _client.GetAsync(new GetAllProduct());

            return View(latestProducts);
        }

        public async Task<IActionResult> AddProduct()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory
            {
                CategoryType = CategoryType.ProductCategory
            }), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (request.SalePrice > request.Price)
            {
                TempData["ErrorMess"] = "Sale Price must smaller than Price";
                return View(request);
            }

            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\products\\{request.ProductName}";

            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

            if (request.ThumbnailFile != null)
            {
                await using Stream fileStream = new FileStream(imageLocalLink, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(fileStream);

                imageLink = $"/uploads/products/{request.ProductName}/{request.ThumbnailFile.FileName}";
            }

            var result = await _client.PostAsync(new PostAddProduct
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Thumbnail = imageLink,
                CategoryId = request.CategoryId
            });

            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Create successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Create fail! {result.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> ViewProduct(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory
            {
                CategoryType = CategoryType.ProductCategory
            }), "Id", "CategoryName");

            ViewData["StatusList"] = new SelectList(listStatus);
            var product = await _client.GetAsync(new GetProductById
            {
                ProductId = id
            });
            return View(product);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(
                new GetAllCategory { CategoryType = CategoryType.ProductCategory }), "Id", "CategoryName");
            ViewData["StatusList"] = new SelectList(listStatus);
            var product = await _client.GetAsync(new GetProductById
            {
                ProductId = id
            });
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (request.SalePrice > request.Price)
            {
                TempData["ErrorMess"] = "Sale Price must smaller than Price";
                return View(request);
            }

            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\products\\{request.ProductName}";

            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            if (request.ThumbnailFile != null)
            {
                var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

                await using Stream fileStream = new FileStream(imageLocalLink, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(fileStream);

                imageLink = $"/uploads/products/{request.ProductName}/{request.ThumbnailFile.FileName}";
            }

            var result = await _client.PutAsync(new PutUpdateProduct
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Thumbnail = string.IsNullOrEmpty(imageLink) ? request.Thumbnail : imageLink,
                CategoryId = request.CategoryId,
                Status = request.Status,
                Id = request.Id
            });

            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Update successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Update fail! {result.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _client.DeleteAsync(new DeleteProduct
            {
                ProductId = id,
                IsHardDelete = true
            });
            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Delete successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMess"] = $"Delete fail! {result.ErrorMessage}";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManagePost()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var latestPosts = await _client.GetAsync(new GetAllPost());
            return View(latestPosts);
        }

        public async Task<IActionResult> AddPost()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory
            {
                CategoryType = CategoryType.PostCategory
            }), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(PostViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\posts\\{request.Title}";

            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

            if (request.ThumbnailFile != null)
            {
                await using Stream fileStream = new FileStream(imageLocalLink, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(fileStream);

                imageLink = $"/uploads/posts/{request.Title}/{request.ThumbnailFile.FileName}";
            }
            var result = await _client.PostAsync(new PostAddPost
            {
                Title = request.Title,
                Featured = request.Featured,
                Brief = request.Brief,
                Description = request.Description,
                Thumbnail = imageLink,
                Author = request.Author,
                CategoryId = request.CategoryId,
            });

            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Create successfully!";
                return RedirectToAction("ManagePost");
            }
            TempData["ErrorMess"] = $"Create fail! {result.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> ViewPost(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory
            {
                CategoryType = CategoryType.PostCategory
            }), "Id", "CategoryName");
            ViewData["StatusList"] = new SelectList(listStatus);

            var post = await _client.GetAsync(new GetPostById
            {
                PostId = id
            });
            return View(post);
        }

        public async Task<IActionResult> EditPost(int id)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory
            {
                CategoryType = CategoryType.PostCategory
            }), "Id", "CategoryName");
            ViewData["StatusList"] = new SelectList(listStatus);

            var post = await _client.GetAsync(new GetPostById
            {
                PostId = id
            });
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(PostViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\posts\\{request.Title}";

            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            if (request.ThumbnailFile != null)
            {
                var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

                await using Stream fileStream = new FileStream(imageLocalLink, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(fileStream);

                imageLink = $"/uploads/posts/{request.Title}/{request.ThumbnailFile.FileName}";
            }

            var result = await _client.PutAsync(new PutUpdatePost
            {
                Id = request.Id,
                Title = request.Title,
                Featured = request.Featured,
                Brief = request.Brief,
                Description = request.Description,
                Thumbnail = string.IsNullOrEmpty(imageLink) ? request.Thumbnail : imageLink,
                Author = request.Author,
                CategoryId = request.CategoryId,
                Status = request.Status
            });

            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Update successfully!";
                return RedirectToAction("ManagePost");
            }
            TempData["ErrorMess"] = $"Update fail! {result.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _client.DeleteAsync(new DeletePost
            {
                PostId = id,
                IsHardDelete = true
            });
            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Delete successfully!";
                return RedirectToAction("ManagePost");
            }
            TempData["ErrorMess"] = $"Delete fail! {result.ErrorMessage}";
            return RedirectToAction("ManagePost");
        }

        public async Task<IActionResult> ManageFeedback()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            //Get all feedback
            var feedback = await _client.GetAsync(new GetAllFeedback());

            return View(feedback);
        }

        public async Task<IActionResult> FeedbackDetail(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["StatusList"] = new SelectList(listStatus);
            //Get feedback
            var feedback = await _client.GetAsync(new GetFeedbackById
            {
                FeedbackId = id
            });

            return View(feedback);
        }

        public async Task<IActionResult> EditFeedback(int id)
        {

            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["StatusList"] = new SelectList(listStatus);

            var feedback = await _client.GetAsync(new GetFeedbackById
            {
                FeedbackId = id
            });

            return View(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> EditFeedback(FeedbackViewModels request)
        {
            //Edit feedback
            var result = await _client.PutAsync(new EditFeedback
            {
                FeedbackId = request.Id,
                Status = request.Status
            });

            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Update successfully!";
                return RedirectToAction("ManageFeedback");
            }
            TempData["ErrorMess"] = $"Update fail! {result.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> ManageSlider()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var sliders = await _client.GetAsync(new GetAllSlider());
            return View(sliders);
        }

        public IActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSlider(SliderViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\sliders\\{request.Title}";

            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

            if (request.ThumbnailFile != null)
            {
                await using Stream fileStream = new FileStream(imageLocalLink, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(fileStream);

                imageLink = $"/uploads/sliders/{request.Title}/{request.ThumbnailFile.FileName}";
            }
            var result = await _client.PostAsync(new PostAddSlider
            {
                Title = request.Title,
                Image = imageLink,
                BlackLink = request.BlackLink,
                Status = Status.Active
            });

            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Create successfully!";
                return RedirectToAction("ManageSlider");
            }
            TempData["ErrorMess"] = $"Create fail! {result.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> ViewSlider(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["StatusList"] = new SelectList(listStatus);

            var slider = await _client.GetAsync(new GetSliderById
            {
                SliderId = id
            });
            return View(slider);
        }

        public async Task<IActionResult> EditSlider(int id)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["StatusList"] = new SelectList(listStatus);

            var slider = await _client.GetAsync(new GetSliderById
            {
                SliderId = id
            });
            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> EditSlider(SliderViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\sliders\\{request.Title}";
            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            if (request.ThumbnailFile != null)
            {
                var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

                await using Stream fileStream = new FileStream(imageLocalLink, FileMode.Create);
                await request.ThumbnailFile.CopyToAsync(fileStream);

                imageLink = $"/uploads/Sliders/{request.Title}/{request.ThumbnailFile.FileName}";
            }

            var result = await _client.PutAsync(new PutUpdateSlider
            {
                Id = request.Id,
                Title = request.Title,
                Image = string.IsNullOrEmpty(imageLink) ? request.Image : imageLink,
                BlackLink = request.BlackLink,
                Status = request.Status

            });

            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Update successfully!";
                return RedirectToAction("ManageSlider");
            }
            TempData["ErrorMess"] = $"Update fail! {result.ErrorMessage}";
            return View(request);
        }

        public async Task<IActionResult> DeleteSlider(int id)
        {
            var result = await _client.DeleteAsync(new DeleteSlider
            {
                SliderId = id,
                IsHardDelete = true
            });
            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Delete successfully!";
                return RedirectToAction("ManageSlider");
            }
            TempData["ErrorMess"] = $"Delete fail! {result.ErrorMessage}";
            return RedirectToAction("ManageSlider");
        }

        public async Task<IActionResult> ManageCustomer()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            //Get all customers
            var customers = await _client.GetAsync(new AccountModels.GetCustomers());

            return View(customers);
        }

        public async Task<IActionResult> EditCustomer(int id, bool lockoutEnable)
        {
            var result = await _client.PutAsync(new AccountModels.UpdateCustomer
            {
                Id = id,
                LockoutEnabled = lockoutEnable
            });
            if (result.StatusCode == Common.Enums.StatusCode.Success)
            {
                TempData["SuccessMess"] = "Update successfully!";
                return RedirectToAction("ManageCustomer");
            }
            TempData["ErrorMess"] = $"Update fail! {result.ErrorMessage}";
            return RedirectToAction("ManageCustomer");
        }

        public async Task<IActionResult> ManageTag()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            //Get all products
            var tags = await _client.GetAsync(new GetAllTag());

            return View(tags);
        }
    }
}
