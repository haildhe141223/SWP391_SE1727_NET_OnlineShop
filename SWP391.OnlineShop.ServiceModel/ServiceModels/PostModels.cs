using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;

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

    [Route("/Post/GetPostByTag", "GET")]
    public class GetPostByTag : IReturn<List<PostViewModel>>
    {
        public int TagId { get; set; }
    }

    [Route("/Post/GetPostByAuthor", "GET")]
    public class GetPostByAuthor : IReturn<List<PostViewModel>>
    {
        public string Author { get; set; }
    }

    [Route("/Post/GetPostByStatus", "GET")]
    public class GetPostByStatus : IReturn<List<PostViewModel>>
    {
        public string Status { get; set; }

    }

    [Route("/Post/GetPostByTitle", "GET")]
    public class GetPostByTitle : IReturn<List<PostViewModel>>
    {
        public string Title { get; set; }

    }


    [Route("/Post/PostAddPost", "POST")]
    public class PostAddPost : IReturn<BaseResultModel>
    {
        public string Title { get; set; }
        public string Featured { get; set; }
        public string Brief { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Author { get; set; }
        public string Tag { get; set; }
        public int? CategoryId { get; set; }

    }

    [Route("/Post/PutUpdatePost", "PUT")]
    public class PutUpdatePost : IReturn<BaseResultModel>
    {
        public string Title { get; set; }
        public string Featured { get; set; }
        public string Brief { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Author { get; set; }
        public string Tag { get; set; }
        public int? CategoryId { get; set; }
        public int Id { get; set; }
        public Status Status { get; set; }
    }

    [Route("/Post/DeletePost", "DELETE")]
    public class DeletePost : IReturn<BaseResultModel>
    {
        public int PostId { get; set; }
        public bool IsHardDelete { get; set; }
    }

	[Route("/Post/GetTagByPost", "GET")]
	public class GetTagByPost : IReturn<List<TagViewModel>>
	{
		public int PostId { get; set; }
	}

	[Route("/Post/GetAllTag", "GET")]
	public class GetAllTag : IReturn<List<TagViewModel>>
	{
	}
}
