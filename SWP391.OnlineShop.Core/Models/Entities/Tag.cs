using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Tag : BaseEntity<int>
{
    public Tag()
    {
        PostTags = new HashSet<PostTag>();
        ProductTags = new HashSet<ProductTag>();
    }

    public string TagName { get; set; }
    public ICollection<PostTag> PostTags { get; set; }
    public ICollection<ProductTag> ProductTags { get; set; }
}