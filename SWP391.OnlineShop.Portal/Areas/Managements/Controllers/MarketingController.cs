﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.FeedbackModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.SliderModel;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
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
            //Get all products
            var latestProducts = await _client.GetAsync(new GetAllProduct());

            return View(latestProducts);
        }

        public async Task<IActionResult> AddProduct()
        {
            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory
            {
                CategoryType = CategoryType.ProductCategory
            }), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel request)
        {
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
            var latestPosts = await _client.GetAsync(new GetAllPost());
            return View(latestPosts);
        }

        public async Task<IActionResult> AddPost()
        {
            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory
            {
                CategoryType = CategoryType.PostCategory
            }), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostViewModel request)
        {
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
            await _client.PutAsync(new EditFeedback
            {
                FeedbackId = request.Id,
                Status = request.Status
            });

            return RedirectToAction("ManageFeedback");
        }

        public async Task<IActionResult> ManageSlider()
        {
            var sliders = await _client.GetAsync(new GetAllSlider());
            return View(sliders);
        }

        public IActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSlider(SliderViewModel request)
        {
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
            await _client.PostAsync(new PostAddSlider
            {
                Title = request.Title,
                Image = imageLink,
                BlackLink = request.BlackLink,
                Status = Status.Active
            });
            return RedirectToAction("ManageSlider");
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
            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\Sliders\\{request.Title}";
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

            await _client.PutAsync(new PutUpdateSlider
            {
                Id = request.Id,
                Title = request.Title,
                Image = string.IsNullOrEmpty(imageLink) ? request.Image : imageLink,
                BlackLink = request.BlackLink,
                Status = request.Status

            });
            return RedirectToAction("ManageSlider");
        }

        public async Task<IActionResult> DeleteSlider(int id)
        {
            await _client.DeleteAsync(new DeleteSlider
            {
                SliderId = id
            });
            return RedirectToAction("ManageSlider");
        }
    }
}
