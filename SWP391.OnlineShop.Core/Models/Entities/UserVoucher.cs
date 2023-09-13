using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class UserVoucher : BaseEntity<int>
{
    public int UserId { get; set; }
    public int VoucherId { get; set; }
    public User User { get; set; }
    public Voucher Voucher { get; set; }
}