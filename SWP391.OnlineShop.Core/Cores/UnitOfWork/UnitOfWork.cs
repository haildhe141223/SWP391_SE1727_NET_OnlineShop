using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Cores.Repositories;

namespace SWP391.OnlineShop.Core.Cores.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(
        OnlineShopContext context)
    {
        Context = context;
        Products = new ProductRepository(Context);
        Categories = new CategoryRepository(Context);
        Emails = new EmailRepository(Context);
        FeedBacks = new FeedBackRepository(Context);
        Orders = new OrderRepository(Context);
        Posts = new PostRepository(Context);
        Tags = new TagRepository(Context);
        Vouchers = new VoucherRepository(Context);
        Settings = new SettingRepository(Context);
        Sliders = new SliderRepository(Context);
    }

    public OnlineShopContext Context { get; }
    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }
    public IEmailRepository Emails { get; }
    public IFeedBackRepository FeedBacks { get; }
    public IOrderRepository Orders { get; }
    public IPostRepository Posts { get; }
    public ITagRepository Tags { get; }
    public IVoucherRepository Vouchers { get; }
    public ISettingRepository Settings { get; }
    public ISliderRepository Sliders { get; }

    public void Dispose()
    {
        Context.Dispose();
    }
    public void Complete()
    {
        Context.BeforeComplete();
        Context.SaveChanges();
    }

    public Task<int> CompleteAsync()
    {
        Context.BeforeComplete();
        return Context.SaveChangesAsync();
    }
}