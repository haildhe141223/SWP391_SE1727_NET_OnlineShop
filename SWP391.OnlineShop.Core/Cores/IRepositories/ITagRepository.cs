﻿using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface ITagRepository : IGenericRepository<Tag, int>
{
    List<Tag> GetallTags();
    Task<string> GetTagById(int id);
    Task<List<Tag>> GetTagByProductId(int productId);
    Task<List<Tag>> GetTagByPostId(int postId);
    Tag GetTagByName(string name);
    Task<List<Tag>> GetTagByStatus(string status);
    Task<List<Tag>> GetTagWithPaging(int skip, int take);
    IEnumerable<int> AddTagByString(string tags);
}