using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;

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
        public async Task<IActionResult> Index()
        {
            var posts = await _client.GetAsync(new GetAllPost());
            foreach (var post in posts)
            {
                var tags = await _client.GetAsync(new GetTagByPost()
                {
                    PostId = post.Id
                });
                post.Tags = tags;
            }
            ViewData["AllTags"] = await _client.GetAsync(new GetAllTag());
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _client.GetAsync(new GetPostById()
            {
                PostId = id
            });
            var tags = await _client.GetAsync(new GetTagByPost()
            {
                PostId = post.Id
            });
            post.Tags = tags;
            ViewData["AllTags"] = await _client.GetAsync(new GetAllTag());
            return View(post);
        }
    }
}
