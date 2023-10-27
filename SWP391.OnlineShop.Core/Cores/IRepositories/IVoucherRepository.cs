using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface IVoucherRepository : IGenericRepository<Voucher, int>
{
    public IEnumerable<Voucher> GetAvailableVouchersOfUser(int userId);

    public IEnumerable<Voucher> GetVouchersCreatedUser(int userId);

    public Voucher GetVoucherInfo(int voucherId);
}