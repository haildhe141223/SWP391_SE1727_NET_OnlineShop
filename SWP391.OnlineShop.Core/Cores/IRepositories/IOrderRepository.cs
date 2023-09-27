using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface IOrderRepository : IGenericRepository<Order, int>
{
    public IEnumerable<Order> GetOrdersByStatus(OrderStatus status);

    public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId);

    public IEnumerable<Order> GetCartDetailByUser(string email);
    public IEnumerable<Order> GetCartContactByUser(string email);
    public IEnumerable<Order> GetCartCompletionByUser(string email);

    public IEnumerable<Order> GetOrdersByUser(string email);
}