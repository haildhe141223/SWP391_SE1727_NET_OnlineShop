using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class ProductVoucher : BaseEntity<int>
{
    public int ProductId { get; set; }
    public int VoucherId { get; set; }

    public Product Product { get; set; }
    public Voucher Voucher { get; set; }
}