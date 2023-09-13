using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class PostTag : BaseEntity<int>
{
    public int PostId { get; set; }
    public int TagId { get; set; }
    public Post Post { get; set; }
    public Tag Tag { get; set; }
}