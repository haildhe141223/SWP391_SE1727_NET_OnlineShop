using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class PostController : BaseController
    {
        private readonly IJsonServiceClient _client;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;
        public PostController(IJsonServiceClient client,
           ILoggerService logger,
           UserManager<User> userManager)
        {
            _client = client;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var posts = new List<PostViewModel>();
            if (string.IsNullOrEmpty(search))
            {
                //get all post
                posts = await _client.GetAsync(new GetAllPost());
            } else
            {
                posts = await _client.GetAsync(new GetPostByTitle
                {
                    Title = search
                });
            }

            ViewBag.Pages = posts.Count % 5 == 0 ? posts.Count / 5 : posts.Count / 5 + 1;
            ViewBag.CurrentPage = page;

            posts = posts.Skip((page - 1) * 5).Take(5).ToList();
            foreach (var post in posts)
            {
            //get tag by post
                var tags = await _client.GetAsync(new GetTagByPost()
                {
                    PostId = post.Id
                });
                post.Tags = tags;
            }
            
            //get all tag
            ViewData["AllTags"] = await _client.GetAsync(new GetAllTag());
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {

        //get post by id
            var post = await _client.GetAsync(new GetPostById()
            {
                PostId = id
            });
        //get tag by post
            var tags = await _client.GetAsync(new GetTagByPost()
            {
                PostId = post.Id
            });
            post.Tags = tags;
        // get all tag
            ViewData["AllTags"] = await _client.GetAsync(new GetAllTag());
            return View(post);
        }
    }
}
