using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class PostRepository : GenericRepository<Post, int>, IPostRepository
{
	public PostRepository(OnlineShopContext context) : base(context)
	{
	}

	public List<Post> GetPostByName(string postName)
	{
		var result = new List<Post>();
		if (Context.Posts == null) return result;

		var posts = Context.Posts.Where(x => x.Title.ToLower().Contains(postName.ToLower()))
			.ToList();

		result = posts.ToList();

		return result;
	}

	public Task<List<Post>> GetPostByStatus(string status)
	{
		var result = new List<Post>();
		if (Context.Posts == null) return Task.FromResult(result);

		var posts = Context.Posts.Where(x => x.Status.ToString() == status)
			.ToList();

		result = posts.ToList();

		return Task.FromResult(result);
	}

	public async Task<string> GetPostNameByPostId(int postId)
	{
		var result = string.Empty;
		if (Context.Posts != null)
		{
			result = await Context.Posts
				 .Where(p => p.Id == postId)
				 .Select(p => p.Title)
				 .FirstOrDefaultAsync();
		}

		return result ?? string.Empty;
	}

	public Task<List<Post>> GetPostsByCategoryId(int categoryId)
	{
		var result = new List<Post>();
		if (Context.Posts == null) return Task.FromResult(result);

		var posts = Context.Posts.Where(x => x.CategoryId == categoryId)
			.ToList();

		result = posts.ToList();

		return Task.FromResult(result);
	}

	public Task<List<Post>> GetPostWithPaging(int skip, int take)
	{
		var result = new List<Post>();
		if (Context.Posts == null) return Task.FromResult(result);

		var posts = Context.Posts.Include(p => p.PostTags).ThenInclude(p => p.Tag).Skip(skip).Take(take)
		   .ToList();

		result = posts.ToList();

		return Task.FromResult(result);
	}

	public override void Add(Post entity)
	{
		if (entity != null && Context.Posts != null)
		{
			Context.Posts.Add(entity);
		}
	}

	public override async Task AddAsync(Post entity)
	{
		if (entity != null && Context.Posts != null)
		{
			await Context.Posts.AddAsync(entity);
		}
	}

	public Task<List<Post>> GetPostsByAuthor(string author)
	{
		var result = new List<Post>();
		if (Context.Posts == null) return Task.FromResult(result);

		var posts = Context.Posts.Where(x => x.Author.ToLower().Equals(author.ToLower()))
			.ToList();

		result = posts.ToList();

		return Task.FromResult(result);
	}

	public List<Post> GetAllPost()
	{
		var result = new List<Post>();
		if (Context.Posts == null) return result;

		var posts = Context.Posts.Include(p => p.PostTags).ThenInclude(p => p.Tag).Include(p => p.Category)
			.OrderByDescending(p => p.CreatedDateTime)
			.ToList();

		result = posts.ToList();

		return result;
	}

	public Task<Post> GetPostById(int id)
	{
		var result = new Post();
		if (Context.Posts == null) return Task.FromResult(result);

		var posts = Context.Posts.Include(p => p.PostTags).ThenInclude(p => p.Tag).Include(p => p.Category)
			.Where(p => p.Id == id);

		result = posts.FirstOrDefault();

		return Task.FromResult(result);
	}
}