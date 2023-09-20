using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface IOrderRepository : IGenericRepository<Order, int>
{
    public IEnumerable<Order> GetOrdersByStatus(OrderStatus status);

    public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId);

    public IEnumerable<Order> GetCartDetail();
    public IEnumerable<Order> GetCartContact();
    public IEnumerable<Order> GetCartCompletion();
}