using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Get all Posts
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get all Posts</returns>
        List<PostViewModel> Get(GetAllPost request);

        /// <summary>
        /// Get Paging Posts
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get Paging Posts</returns>
        List<PostViewModel> Get(GetPagingPost request);

        /// <summary>
        /// Get Post By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get Post By Id</returns>
        PostViewModel Get(GetPostById request);

        /// <summary>
        /// Get Post By Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get Post By Category</returns>
        List<PostViewModel> Get(GetPostByCategory request);

        /// <summary>
        /// Get Post By Author
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get Post By Author</returns>
        List<PostViewModel> Get(GetPostByAuthor request);

        /// <summary>
        /// Get Post by Status
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get Post by Status</returns>
        List<PostViewModel> Get(GetPostByStatus request);

        /// <summary>
        /// Get Post by Title
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get Post by Title</returns>
        List<PostViewModel> Get(GetPostByTitle request);

        /// <summary>
        /// Post Add new Post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResultModel> Post(PostAddPost request);

        /// <summary>
        /// Delete Post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResultModel> Delete(DeletePost request);

        /// <summary>
        /// Update Post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResultModel> Put(PutUpdatePost request);

        /// <summary>
        /// Get Tag by Post
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of tag</returns>
		List<TagViewModel> Get(GetTagByPost request);

        /// <summary>
        /// Get All Tag
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of tag</returns>
		List<TagViewModel> Get(GetAllTag request);
	}
}
