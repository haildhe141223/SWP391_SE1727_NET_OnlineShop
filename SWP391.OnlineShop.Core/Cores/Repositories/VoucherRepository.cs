using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class VoucherRepository : GenericRepository<Voucher, int>, IVoucherRepository
{
    public VoucherRepository(OnlineShopContext context) : base(context)
    {
    }

    public IEnumerable<Voucher> GetAvailableVouchersOfUser(int userId)
    {
        var result = new List<Voucher>();
        var userVoucher = Context.UserVouchers
            .Where(uv => uv.UserId == userId)
            .Include(uv => uv.Voucher)
            .ToList();

        if (userVoucher.Count > 0)
        {
            foreach (var voucher in userVoucher)
            {
                if (voucher.Voucher.CreatedDateTime <= DateTime.Now && voucher.Voucher.EndDateTime >= DateTime.Today)
                {
                    result.Add(voucher.Voucher);
                }
            }
        }
        return result;
    }

    public Voucher GetVoucherInfo(int voucherId)
    {
        var result = new Voucher();
        var voucher = Context.Vouchers.Include(v => v.ProductVouchers).ThenInclude(p => p.Product).Where(v => v.Id == voucherId).FirstOrDefault();
        if (voucher == null)
        {
            return result;
        }
        return voucher;
    }

    public IEnumerable<Voucher> GetVouchersCreatedUser(int userId)
    {
        var result = new List<Voucher>();
        var vouchers = Context.Vouchers.Include(v => v.ProductVouchers).ThenInclude(p => p.Product).Where(v => v.CreatedBy == userId).ToList();
        if (!vouchers.Any())
        {
            return result;
        }
        return vouchers;
    }
}