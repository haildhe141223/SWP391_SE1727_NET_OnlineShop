using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.IRepositories;

namespace SWP391.OnlineShop.Core.Cores.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    OnlineShopContext Context { get; }
    IProductRepository Products { get; }
    void Complete();
    Task<int> CompleteAsync();
}