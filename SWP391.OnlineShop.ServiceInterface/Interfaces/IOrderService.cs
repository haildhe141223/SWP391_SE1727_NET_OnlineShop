using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Cart;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Get Cart Detail By User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Order Model</returns>
        OrderViewModel Get(GetCartDetailByUser request);

		/// <summary>
		/// Get Cart Contact By User
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Order Model</returns>
		OrderViewModel Get(GetCartContactByUser request);

		/// <summary>
		/// Get Cart Completion By User
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Order Model</returns>
		OrderViewModel Get(GetCartCompletionByUser request);

		/// <summary>
		/// Get Cart Information
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Order Model</returns>
		OrderViewModel Get(GetCartInfo request);

		/// <summary>
		/// Add Product to cart
		/// </summary>
		/// <param name="request"></param>
		/// <returns>order</returns>
		Task<OrderViewModel> Post(PostAddToCart request);

		/// <summary>
		/// Update Cart
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Order</returns>
        Task<OrderViewModel> Put(PutUpdateCart request);

		/// <summary>
		/// Update Cart to Contact
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Order</returns>
        Task<OrderViewModel> Put(PutUpdateCartToContact request);

		/// <summary>
		/// Update Quantity
		/// </summary>
		/// <param name="request"></param>
		/// <returns>API Status Code</returns>
        Task<BaseResultModel> Put(PutUpdateQuantity request);

		/// <summary>
		/// Delete cart
		/// </summary>
		/// <param name="request"></param>
		/// <returns>API Status Code</returns>
		Task<BaseResultModel> Delete(DeleteCart request);

		/// <summary>
		/// Delete Order Detail
		/// </summary>
		/// <param name="request"></param>
		/// <returns>API Status Code</returns>
		Task<BaseResultModel> Delete(DeleteOrderDetail request);
	}
}
