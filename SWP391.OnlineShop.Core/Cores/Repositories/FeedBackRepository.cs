using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class FeedBackRepository : GenericRepository<FeedBack, int>, IFeedBackRepository
{
    public FeedBackRepository(OnlineShopContext context) : base(context)
    {
    }

    public async Task<string> GetFeedbackById(int Id)
    {
        var result = string.Empty;
        if (Context.FeedBacks != null)
        {
            result = await Context.FeedBacks
                 .Where(p => p.Id == Id)
                 .Select(p => p.Comment)
                 .FirstOrDefaultAsync();
        }

        return result ?? string.Empty;
    }

    public Task<List<FeedBack>> GetFeedbackByName(string name)
    {
        var result = new List<FeedBack>();
        if (Context.FeedBacks == null) return Task.FromResult(result);

        var feedbacks = Context.FeedBacks.Where(x => x.Comment.ToLower().Contains(name.ToLower()))
            .ToList();

        result = feedbacks.ToList();

        return Task.FromResult(result);
    }

    public Task<List<FeedBack>> GetFeedbackByProductId(int productId)
    {
        var result = new List<FeedBack>();
        if (Context.FeedBacks != null)
        {
            result = Context.FeedBacks
                 .Where(p => p.ProductId == productId)
                 .ToList();
        }

        return Task.FromResult(result);
    }

    public Task<List<FeedBack>> GetFeedbackByStatus(string status)
    {
        var result = new List<FeedBack>();
        if (Context.FeedBacks == null) return Task.FromResult(result);

        var feedbacks = Context.FeedBacks.Where(x => x.Status.ToString() == status)
            .ToList();

        result = feedbacks.ToList();

        return Task.FromResult(result);
    }

    public Task<List<FeedBack>> GetPostWithPaging(int skip, int take)
    {
        var result = new List<FeedBack>();
        if (Context.FeedBacks == null) return Task.FromResult(result);

        var feedBacks = Context.FeedBacks.Skip(skip).Take(take)
            .ToList();

        result = feedBacks.ToList();

        return Task.FromResult(result); throw new NotImplementedException();
    }
}