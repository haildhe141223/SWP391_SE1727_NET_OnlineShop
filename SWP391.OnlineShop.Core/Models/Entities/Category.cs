using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Category : BaseEntity<int>
{
    public Category()
    {
        Posts = new HashSet<Post>();
        Products = new HashSet<Product>();
    }

    public string CategoryName { get; set; }
    public CategoryType CategoryType { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Product> Products { get; set; }
}