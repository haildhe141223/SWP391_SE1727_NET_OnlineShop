using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Voucher : BaseEntity<int>
{
    public Voucher()
    {
        UserVouchers = new HashSet<UserVoucher>();
        OrderVouchers = new HashSet<OrderVoucher>();
    }

    public string VoucherName { get; set; }
    public string VoucherCode { get; set; }
    public int Amount { get; set; }
    public int CreatedBy { get; set; }
    public User User { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public VoucherType Type { get; set; }
    public decimal Value { get; set; }
    public ICollection<OrderVoucher> OrderVouchers { get; set; }
    public ICollection<UserVoucher> UserVouchers { get; set; }
}