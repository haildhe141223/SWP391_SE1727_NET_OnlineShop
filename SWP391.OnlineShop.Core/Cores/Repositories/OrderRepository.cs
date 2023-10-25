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
        var result = Context.Orders
            .Include(o => o.OrderDetails.Where(od => od.Status == Status.Active)).
            ThenInclude(od => od.Product).
            Include(o => o.User).
            Where(o => o.OrderStatus == OrderStatus.InCartCompletion
            && o.Status == Status.Active
            && o.User.Email.Equals(email)).
            ToList();
        return result;
    }

    public IEnumerable<Order> GetCartContactByUser(string email)
    {
        var result = Context.Orders.
            Include(o => o.OrderDetails.Where(od => od.Status == Status.Active)).
            ThenInclude(od => od.Product).
            Include(o => o.User).
            Where(o => o.OrderStatus == OrderStatus.InCartContact
              && o.Status == Status.Active
            && o.User.Email.Equals(email)).
            ToList();
        return result;
    }

    public IEnumerable<Order> GetCartDetailByUser(string email)
    {
        var query = Context.Orders.
            Include(o => o.OrderDetails.Where(od => od.Status == Status.Active)).
            ThenInclude(od => od.Product).
            Include(o => o.User);
        var result = query.Where(o => o.OrderStatus == OrderStatus.InCartDetail
            && o.Status == Status.Active
            && o.User.Email.Equals(email)).
            ToList();
        return result;
    }

    public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId)
    {
        var result = Context.OrderDetails.
            Where(od => od.OrderId == orderId
            && od.Status == Status.Active).
            ToList();
        return result;
    }

    public OrderDetail GetOrderDetailByOrderDetailId(int orderDetailId)
    {
        var result = Context.OrderDetails.
            Include(od => od.Product).
            FirstOrDefault(od => od.Id == orderDetailId
                                 && od.Status == Status.Active);
        return result;
    }

    public IEnumerable<Order> GetOrdersByStatus(OrderStatus status)
    {
        var result = Context.Orders
            .Include(o => o.OrderDetails.Where(od => od.Status == Status.Active)).
            ThenInclude(od => od.Product)
            .Where(o => o.OrderStatus == status
            && o.Status == Status.Active)
            .ToList();
        return result;
    }

    public IEnumerable<Order> GetOrdersByUser(string email)
    {
        var result = Context.Orders.
            Include(o => o.OrderDetails.Where(od => od.Status == Status.Active)).
            ThenInclude(o => o.Product).
            Include(o => o.User).
            Where(o => o.OrderStatus != OrderStatus.InCartDetail
            && o.OrderStatus != OrderStatus.InCartContact
            && o.OrderStatus != OrderStatus.InCartCompletion
             && o.Status == Status.Active
            && o.User.Email.Equals(email)).
            ToList();
        return result;
    }

    public void DeleteOrderDetail(int id)
    {
        var orderDetail = Context.OrderDetails.Find(id);
        if (orderDetail != null) orderDetail.Status = Status.Inactive;
    }

    public void UpdateOrderDetailQuantity(int id, int quantity)
    {
        var orderDetail = Context.OrderDetails.Find(id);
        if (orderDetail != null) orderDetail.Quantity = quantity;
    }

    public Order GetOrderInfoById(int id)
    {
        var result = Context.Orders
            .Include(o => o.OrderDetails.Where(od => od.Status == Status.Active))
            .ThenInclude(o => o.Product).ThenInclude(p => p.Category).FirstOrDefault(o => o.Id == id && o.Status == Status.Active);
        return result;
    }

    public void UpdateOrderStatusBy(int id, OrderStatus orderStatus)
    {
        var orderDetail = Context.OrderDetails.Find(id);
        if (orderDetail != null) orderDetail.OrderStatus = orderStatus;
    }

}