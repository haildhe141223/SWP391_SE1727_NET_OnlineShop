using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.IRepositories;

namespace SWP391.OnlineShop.Core.Cores.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    OnlineShopContext Context { get; }
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IEmailRepository Emails { get; }
    IFeedBackRepository FeedBacks { get; }
    IOrderRepository Orders { get; }
    IPostRepository Posts { get; }
    ITagRepository Tags { get; }
    IVoucherRepository Vouchers { get; }
    ISettingRepository Settings { get; }
    ISliderRepository Sliders { get; }

    void Complete();
    Task<int> CompleteAsync();
}