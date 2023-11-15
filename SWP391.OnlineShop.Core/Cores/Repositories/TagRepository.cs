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



    public Tag GetTagByName(string name)
    {
        var result = new Tag();
        if (Context.Tags == null) return result;

        var tag = Context.Tags.Include(x => x.ProductTags).Where(x => x.TagName.ToLower().Equals(name.ToLower()))
            .FirstOrDefault();

        result = tag;

        return result;
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

    public List<Tag> GetallTags()
    {
        var result = new List<Tag>();
        if (Context.Tags == null) return result;

        var tags = Context.Tags.Include(x => x.ProductTags).ThenInclude(a => a.Product)
            .ToList();

        result = tags.ToList();

        return result;
    }

    public IEnumerable<int> AddTagByString(string tags)
    {
        var tagsName = tags.Split(';');
        foreach (var item in tagsName)
        {
            var tagExist = Context.Tags.Where(t => t.TagName.Trim().ToLower() == item.Trim().ToLower()).Count();
            if (tagExist == 0)
            {
                var tag = new Tag()
                {
                    TagName = item,
                };
                Context.Tags.Add(tag);
            }
        }
        Context.SaveChanges();

        foreach (var item in tagsName)
        {
            var tagExist = Context.Tags.FirstOrDefault(t => t.TagName.Trim().ToLower() == item.Trim().ToLower());
            if (tagExist != null)
            {
                yield return tagExist.Id;
            }
        }
    }
}