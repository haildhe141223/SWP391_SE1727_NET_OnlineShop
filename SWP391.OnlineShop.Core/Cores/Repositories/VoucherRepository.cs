using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class VoucherRepository : GenericRepository<Voucher, int>, IVoucherRepository
{
    public VoucherRepository(OnlineShopContext context) : base(context)
    {
    }

    public IEnumerable<Voucher> GetAvailableVouchers()
    {
        var result = new List<Voucher>();
        var userVoucher = Context.Vouchers.Include(v => v.UserVouchers)
            .Where(uv => uv.Status == Status.Active)
            .ToList();

        if (userVoucher.Count <= 0) return result;

        foreach (var voucher in userVoucher)
        {
            if (voucher.StartDateTime <= DateTime.Now && voucher.EndDateTime >= DateTime.Today && voucher.Amount > 0 && !voucher.UserVouchers.Any())
            {
                result.Add(voucher);
            }
        }
        return result;
    }

    public IEnumerable<Voucher> GetAvailableVouchersOfUser(int userId)
    {
        var result = new List<Voucher>();
        var userVoucher = Context.UserVouchers
            .Where(uv => uv.UserId == userId && uv.Status == Status.Active)
            .Include(uv => uv.Voucher)
            .ToList();

        if (userVoucher.Count <= 0) return result;

        foreach (var voucher in userVoucher)
        {
            if (voucher.Voucher.StartDateTime <= DateTime.Now && voucher.Voucher.EndDateTime >= DateTime.Today && voucher.Voucher.Amount > 0)
            {
                result.Add(voucher.Voucher);
            }
        }
        return result;
    }

    public Voucher GetVoucherInfo(int voucherId)
    {
        var result = new Voucher();
        var voucher = Context.Vouchers
            .Include(v => v.OrderVouchers)
            .ThenInclude(p => p.Order)
            .FirstOrDefault(v => v.Id == voucherId && v.Status == Status.Active);

        return voucher ?? result;
    }

    public IEnumerable<Voucher> GetVouchersCreatedUser(int userId)
    {
        var vouchers = Context.Vouchers
            .Include(v => v.OrderVouchers)
            .ThenInclude(p => p.Order)
            .Where(v => v.CreatedBy == userId && v.Status == Status.Active)
            .ToList();

        return !vouchers.Any() ? new List<Voucher>() : vouchers;
    }
}