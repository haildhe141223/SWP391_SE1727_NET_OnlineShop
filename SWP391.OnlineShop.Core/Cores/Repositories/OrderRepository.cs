using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Contexts;
using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Cores.IRepositories;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Cores.Repositories;

public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
{
    public OrderRepository(OnlineShopContext context) : base(context)
    {
    }

    public IEnumerable<Order> GetCartCompletionByUser(string email)
    {
        var result = Context.Orders.
            Include(o => o.OrderDetails).
            ThenInclude(od => od.Product).
            Include(o => o.User).
            Where(o => o.OrderStatus == OrderStatus.InCartCompletion
            && o.User.Email.Equals(email)).
            ToList();
        return result;
    }

    public IEnumerable<Order> GetCartContactByUser(string email)
    {
        var result = Context.Orders.
            Include(o => o.OrderDetails).
            ThenInclude(od => od.Product).
            Include(o => o.User).
            Where(o => o.OrderStatus == OrderStatus.InCartContact
            && o.User.Email.Equals(email)).
            ToList();
        return result;
    }

    public IEnumerable<Order> GetCartDetailByUser(string email)
    {
        var result = Context.Orders.
            Include(o => o.OrderDetails).
            ThenInclude(od => od.Product).
            Include(o => o.User).
            Where(o => o.OrderStatus == OrderStatus.InCartDetail).
            ToList();
        return result;
    }

    public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId)
    {
        var result = Context.OrderDetails.
            Where(od => od.OrderId == orderId).
            ToList();
        return result;
    }

    public IEnumerable<Order> GetOrdersByStatus(OrderStatus status)
    {
        var result = Context.Orders
            .Include(o => o.OrderDetails).
            ThenInclude(od => od.Product)
            .Where(o => o.OrderStatus == status)
            .ToList();
        return result;
    }

    public IEnumerable<Order> GetOrdersByUser(string email)
    {
        var result = Context.Orders.
            Include(o => o.OrderDetails).
            Include(o => o.User).
            Where(o => o.OrderStatus == OrderStatus.InCartDetail
            && o.User.Email.Equals(email)).
            ToList();
        throw new NotImplementedException();
    }
}