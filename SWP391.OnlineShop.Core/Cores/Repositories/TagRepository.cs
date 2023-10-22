using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class TagRepository : GenericRepository<Tag, int>, ITagRepository
{
    public TagRepository(OnlineShopContext context) : base(context)
    {
    }


    public async Task<string> GetTagById(int id)
    {
        var result = string.Empty;
        if (Context.Tags != null)
        {
            result = await Context.Tags
                 .Where(p => p.Id == id)
                 .Select(p => p.TagName)
                 .FirstOrDefaultAsync();
        }

        return result ?? string.Empty;
    }



    public Task<List<Tag>> GetTagByName(string name)
    {
        var result = new List<Tag>();
        if (Context.Tags == null) return Task.FromResult(result);

        var tags = Context.Tags.Where(x => x.TagName.ToLower().Contains(name.ToLower()))
            .ToList();

        result = tags.ToList();

        return Task.FromResult(result);
    }



    public Task<List<Tag>> GetTagByProductId(int productId)
    {
        var result = new List<Tag>();
        if (Context.Tags == null) return Task.FromResult(result);

        var tags = Context.Tags.Include(x => x.ProductTags.Where(p => p.ProductId == productId))
            .ToList();

        result = tags.ToList();

        return Task.FromResult(result);
    }



    public Task<List<Tag>> GetTagByPostId(int postId)
    {
        var result = new List<Tag>();
        if (Context.Tags == null) return Task.FromResult(result);

        var tags = Context.Tags.Include(x => x.PostTags.Where(p => p.PostId == postId))
            .ToList();

        result = tags.ToList();

        return Task.FromResult(result);
    }

    public Task<List<Tag>> GetTagByStatus(string status)
    {
        var result = new List<Tag>();
        if (Context.Tags == null) return Task.FromResult(result);

        var tags = Context.Tags.Where(x => x.Status.ToString() == status)
            .ToList();

        result = tags.ToList();

        return Task.FromResult(result);
    }



    public Task<List<Tag>> GetTagWithPaging(int skip, int take)
    {
        var result = new List<Tag>();
        if (Context.Tags == null) return Task.FromResult(result);

        var tags = Context.Tags.Skip(skip).Take(take)
            .ToList();

        result = tags.ToList();

        return Task.FromResult(result);
    }
}