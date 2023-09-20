using Microsoft.EntityFrameworkCore;
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

    public IEnumerable<Voucher> GetAvailableVouchersOfUser(int userId)
    {
        var result = new List<Voucher>();
        var userVoucher = Context.UserVouchers.Where(uv => uv.UserId == userId).Include(uv =>uv.Voucher).ToList();
        if(userVoucher != null && userVoucher.Count > 0)
        {
            foreach(var voucher in userVoucher)
            {
                if(voucher.Voucher.CreatedDateTime <= DateTime.Now && voucher.Voucher.EndDateTime >= DateTime.Today)
                {
                    result.Add(voucher.Voucher);
                }
            }
        }
        return result;
    }
}