using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class VoucherRepository : GenericRepository<Voucher, int>, IVoucherRepository
{
    public VoucherRepository(OnlineShopContext context) : base(context)
    {
    }
}