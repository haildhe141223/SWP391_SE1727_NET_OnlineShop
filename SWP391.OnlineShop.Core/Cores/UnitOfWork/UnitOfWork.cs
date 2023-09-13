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
    }

    public OnlineShopContext Context { get; }
    public IProductRepository Products { get; }

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