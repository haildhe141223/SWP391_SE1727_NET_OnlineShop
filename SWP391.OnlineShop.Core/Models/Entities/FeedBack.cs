using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class FeedBack : BaseEntity<int>
{
    public decimal RatedStar { get; set; }
    public string Comment { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}