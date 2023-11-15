using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class OrderVoucher : BaseEntity<int>
{
    public int OrderId { get; set; }
    public int VoucherId { get; set; }

    public Order Order { get; set; }
    public Voucher Voucher { get; set; }
}