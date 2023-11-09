﻿using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface IPostRepository : IGenericRepository<Post, int>
{
    Task<string> GetPostNameByPostId(int postId);
    Task<List<Post>> GetPostsByCategoryId(int categoryId);
    Task<List<Post>> GetPostsByAuthor(string author);
    List<Post> GetPostByName(string postName);
    Task<List<Post>> GetPostByStatus(string status);
    Task<List<Post>> GetPostWithPaging(int skip, int take);
	List<Post> GetAllPost();
	Task<Post> GetPostById(int id);
}