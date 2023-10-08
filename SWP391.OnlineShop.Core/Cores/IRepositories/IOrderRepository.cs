using SWP391.OnlineShop.Core.Cores.Infrastructures;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Cores.IRepositories;

public interface IOrderRepository : IGenericRepository<Order, int>
{
	IEnumerable<Order> GetOrdersByStatus(OrderStatus status);

	IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId);

	IEnumerable<Order> GetCartDetailByUser(string email);
	IEnumerable<Order> GetCartContactByUser(string email);
	IEnumerable<Order> GetCartCompletionByUser(string email);

	IEnumerable<Order> GetOrdersByUser(string email);
	void DeleteOrderDetail(int id);
	void UpdateOrderDetailQuantity(int id, int quantity);
	OrderDetail GetOrderDetailByOrderDetailId(int orderDetailId);
}