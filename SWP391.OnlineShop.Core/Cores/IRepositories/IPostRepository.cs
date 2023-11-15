using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface IPostRepository : IGenericRepository<Post, int>
{
    Task<string> GetPostNameByPostId(int postId);
    List<Post> GetPostsByCategoryId(int categoryId);
    List<Post> GetPostsTagId(int tagId);
    Task<List<Post>> GetPostsByAuthor(string author);
    List<Post> GetPostByName(string postName);
    Task<List<Post>> GetPostByStatus(string status);
    Task<List<Post>> GetPostWithPaging(int skip, int take);
	List<Post> GetAllPost();
	Post GetPostById(int id);
}