using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{

    [Route("/Post/GetAllPost", "GET")]
    public class GetAllPost : IReturn<List<PostViewModel>>
    {

    }

    [Route("/Post/GetPagingPost", "GET")]
    public class GetPagingPost : IReturn<List<PostViewModel>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    [Route("/Post/GetPostById", "GET")]
    public class GetPostById : IReturn<PostViewModel>
    {
        public int PostId { get; set; }
    }

    [Route("/Post/GetPostByCategory", "GET")]
    public class GetPostByCategory : IReturn<List<PostViewModel>>
    {
        public int CategoryId { get; set; }
    }

    [Route("/Post/GetPostByAuthor", "GET")]
    public class GetPostByAuthor : IReturn<List<PostViewModel>>
    {
        public string? Author { get; set; }
    }

    [Route("/Post/GetPostByStatus", "GET")]
    public class GetPostByStatus : IReturn<List<PostViewModel>>
    {
        public string? Status { get; set; }

    }

    [Route("/Post/GetPostByTitle", "GET")]
    public class GetPostByTitle : IReturn<List<PostViewModel>>
    {
        public string? Title { get; set; }

    }


    [Route("/Post/PostAddPost", "POST")]
    public class PostAddPost : IReturn<PostViewModel>
    {
        public string? Title { get; set; }
        public string? Featured { get; set; }
        public string? Brief { get; set; }
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public string? Author { get; set; }
        public int? CategoryId { get; set; }

    }

    [Route("/Post/PutUpdatePost", "PUT")]
    public class PutUpdatePost : IReturn<PostViewModel>
    {

        public string? Title { get; set; }
        public string? Featured { get; set; }
        public string? Brief { get; set; }
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public string? Author { get; set; }
        public int? CategoryId { get; set; }
        public int Id { get; set; }
    }

    [Route("/Post/DeletePost", "DELETE")]
    public class DeletePost : IReturn<PostViewModel>
    {
        public int PostId { get; set; }
    }

}
