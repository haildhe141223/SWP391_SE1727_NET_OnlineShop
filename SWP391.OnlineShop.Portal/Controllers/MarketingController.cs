﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceStack;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class MarketingController : Controller
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
            ViewData["GenreList"] = new SelectList(await _client.GetAsync(new GetAllCategory()), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel request)
        {
            await _client.PostAsync(new PostAddProduct
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Thumbnail = request.Thumbnail,
                //CategoryId = request.CategoryId,
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewProduct(int id)
        {

            var product = await _client.GetAsync(new GetProductById
            {
                ProductId = id
            });
            return View(product);
        }

        public async Task<IActionResult> EditProduct(int id)
        {

            var product = await _client.GetAsync(new GetProductById
            {
                ProductId = id
            });
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel request)
        {
            await _client.PutAsync(new PutUpdateProduct
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Thumbnail = request.Thumbnail,
                CategoryId = request.CategoryId
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _client.DeleteAsync(new DeleteProduct
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

        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostViewModel request)
        {
            await _client.PostAsync(new PostAddPost
            {
                Title = request.Title,
                Featured = request.Featured,
                Brief = request.Brief,
                Description = request.Description,
                Thumbnail = request.Thumbnail,
                Author = request.Author
            });
            return RedirectToAction("ManagePost");
        }

        public async Task<IActionResult> ViewPost(int id)
        {

            var product = await _client.GetAsync(new GetPostById
            {
                PostId = id
            });
            return View(product);
        }

        public async Task<IActionResult> EditPost(int id)
        {

            var product = await _client.GetAsync(new GetPostById
            {
                PostId = id
            });
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(PostViewModel request)
        {
            await _client.PutAsync(new PutUpdatePost
            {
                Title = request.Title,
                Featured = request.Featured,
                Brief = request.Brief,
                Description = request.Description,
                Thumbnail = request.Thumbnail,
                Author = request.Author
            });
            return RedirectToAction("ManagePost");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var product = await _client.DeleteAsync(new DeletePost
            {
                PostId = id
            });
            return RedirectToAction("ManagePost");
        }

    }
}
