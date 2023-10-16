using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Order : BaseEntity<int>
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    public DateTime OrderDateTime { get; set; }
    public int CustomerId { get; set; }
    public User User { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public Gender CustomerGender { get; set; }
    public string CustomerEmail { get; set; }
    public decimal TotalCost { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public string? OrderNotes { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
}