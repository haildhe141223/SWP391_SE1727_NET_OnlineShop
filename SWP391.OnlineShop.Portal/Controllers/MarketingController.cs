using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.FeedbackModels;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class MarketingController : BaseController
    {
        private readonly IJsonServiceClient _client;
        private readonly ILoggerService _logger;

        public MarketingController(
            IJsonServiceClient client,
        ILoggerService logger)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInfo("1. Home Index - Start");

            //Get all products
            var latestProducts = await _client.GetAsync(new GetAllProduct());

            return View(latestProducts);
        }

        public async Task<IActionResult> AddProduct()
        {
            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory { CategoryType = CategoryType.ProductCategory }), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel request)
        {
            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\Uploads\\{request.ProductName}";
            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

            if (request.ThumbnailFile != null)
            {
                using (Stream fileStream = new FileStream(imageLocalLink, FileMode.Create))
                {
                    await request.ThumbnailFile.CopyToAsync(fileStream);
                }

                imageLink = $"/Uploads//{request.ProductName}/{request.ThumbnailFile.FileName}";
            }

            await _client.PostAsync(new PostAddProduct
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Thumbnail = imageLink,
                CategoryId = request.CategoryId
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewProduct(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory { CategoryType = CategoryType.ProductCategory }), "Id", "CategoryName");
            ViewData["StatusList"] = new SelectList(listStatus);
            var product = await _client.GetAsync(new GetProductById
            {
                ProductId = id
            });
            return View(product);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory { CategoryType = CategoryType.ProductCategory }), "Id", "CategoryName");
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
            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\Uploads\\{request.ProductName}";
            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            if (request.ThumbnailFile != null)
            {
                var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

                using (Stream fileStream = new FileStream(imageLocalLink, FileMode.Create))
                {
                    await request.ThumbnailFile.CopyToAsync(fileStream);
                }

                imageLink = $"/Uploads//{request.ProductName}/{request.ThumbnailFile.FileName}";
            }

            await _client.PutAsync(new PutUpdateProduct
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
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _client.DeleteAsync(new DeleteProduct
            {
                ProductId = id
            });
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ManagePost()
        {
            _logger.LogInfo("1. Post Index - Start");

            //Get all products
            var latestPosts = await _client.GetAsync(new GetAllPost());

            return View(latestPosts);
        }

        public async Task<ActionResult> AddPost()
        {
            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory { CategoryType = CategoryType.PostCategory }), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostViewModel request)
        {
            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\Uploads\\{request.Title}";
            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

            if (request.ThumbnailFile != null)
            {
                using (Stream fileStream = new FileStream(imageLocalLink, FileMode.Create))
                {
                    await request.ThumbnailFile.CopyToAsync(fileStream);
                }

                imageLink = $"/Uploads//{request.Title}/{request.ThumbnailFile.FileName}";
            }
            await _client.PostAsync(new PostAddPost
            {
                Title = request.Title,
                Featured = request.Featured,
                Brief = request.Brief,
                Description = request.Description,
                Thumbnail = imageLink,
                Author = request.Author,
                CategoryId = request.CategoryId,
            });
            return RedirectToAction("ManagePost");
        }

        public async Task<IActionResult> ViewPost(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory { CategoryType = CategoryType.PostCategory }), "Id", "CategoryName");
            ViewData["StatusList"] = new SelectList(listStatus);

            var post = await _client.GetAsync(new GetPostById
            {
                PostId = id
            });
            return View(post);
        }

        public async Task<IActionResult> EditPost(int id)
        {
            var listStatus = new List<Status>
            {
                Status.Active,
                Status.Inactive
            };

            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory { CategoryType = CategoryType.PostCategory }), "Id", "CategoryName");
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
            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\Uploads\\{request.Title}";
            var imageLink = string.Empty;
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            if (request.ThumbnailFile != null)
            {
                var imageLocalLink = Path.Combine(imageFolderLink, request.ThumbnailFile.FileName);

                using (Stream fileStream = new FileStream(imageLocalLink, FileMode.Create))
                {
                    await request.ThumbnailFile.CopyToAsync(fileStream);
                }

                imageLink = $"/Uploads//{request.Title}/{request.ThumbnailFile.FileName}";
            }

            await _client.PutAsync(new PutUpdatePost
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
            return RedirectToAction("ManagePost");
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            await _client.DeleteAsync(new DeletePost
            {
                PostId = id
            });
            return RedirectToAction("ManagePost");
        }

        public async Task<IActionResult> ManageFeedback()
        {
            //Get all feedbacks
            var feedbacks = await _client.GetAsync(new GetAllFeedback());

            return View(feedbacks);
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
            //Get feedback
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
            await _client.PutAsync(new EditFeedback
            {
                FeedbackId = request.Id,
                Status = request.Status
            });

            return RedirectToAction("ManageFeedback");
        }

    }
}
