using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Product : BaseEntity<int>
{
    public Product()
    {
        ProductTags = new HashSet<ProductTag>();
        FeedBacks = new HashSet<FeedBack>();
        OrderDetails = new HashSet<OrderDetail>();
        ProductSizes = new HashSet<ProductSize>();
    }

    public string ProductName { get; set; }
    public string Thumbnail { get; set; }
    public string Description { get; set; }
    public ProductType ProductType { get; set; }
    public decimal Price { get; set; }
    public decimal SalePrice { get; set; }
    public int? CategoryId { get; set; }

    public Category Category { get; set; }

    public ICollection<FeedBack> FeedBacks { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<ProductTag> ProductTags { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; }
}