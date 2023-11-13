using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class ProductSize : BaseEntity<int>
{
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int Quantity { get; set; }

    public Product Product { get; set; }
    public Size Size { get; set; }
}