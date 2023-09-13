using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class ProductTag : BaseEntity<int>
{
    public int ProductId { get; set; }
    public int TagId { get; set; }

    public Product Product { get; set; }
    public Tag Tag { get; set; }
}