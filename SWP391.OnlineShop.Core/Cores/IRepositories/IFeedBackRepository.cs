using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface IFeedBackRepository : IGenericRepository<FeedBack, int>
{
    Task<string> GetFeedbackById(int Id);
    Task<List<FeedBack>> GetFeedbackByProductId(int productId);

    Task<List<FeedBack>> GetFeedbackByName(string name);

    Task<List<FeedBack>> GetFeedbackByStatus(string status);

    Task<List<FeedBack>> GetPostWithPaging(int skip, int take);
}