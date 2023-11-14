using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.IRepositories
{
    public interface ISizeRepository : IGenericRepository<Size, int>
    {
        Size GetSizeByName(string sizeType);
    }
}
