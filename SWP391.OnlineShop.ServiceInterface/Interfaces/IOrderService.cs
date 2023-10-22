using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
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
        OrderViewModels Get(GetCartDetailByUser request);

        /// <summary>
        /// Get Cart Contact By User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Order Model</returns>
        OrderViewModels Get(GetCartContactByUser request);

        /// <summary>
        /// Get Cart Completion By User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Order Model</returns>
        OrderViewModels Get(GetCartCompletionByUser request);

        /// <summary>
        /// Get Cart Information
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Order Model</returns>
        OrderViewModels Get(GetCartInfo request);

        /// <summary>
        /// Add Product to cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns>order</returns>
        Task<OrderViewModels> Post(PostAddToCart request);

        /// <summary>
        /// Update Cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Order</returns>
        Task<OrderViewModels> Put(PutUpdateCart request);

        /// <summary>
        /// Update Cart to Contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Order</returns>
        Task<OrderViewModels> Put(PutUpdateCartToContact request);

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

        /// <summary>
        /// Update Cart Status
        /// </summary>
        /// <param name="request"></param>
        /// <returns>API Status Code</returns>
        Task<BaseResultModel> Put(PutUpdateCartStatus request);

        /// <summary>
        /// Update Cart Notes
        /// </summary>
        /// <param name="request"></param>
        /// <returns>API Status Code</returns>
        Task<BaseResultModel> Put(PutUpdateOrderNotes request);

        /// <summary>
        /// Get All order by user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>list of orders</returns>
        List<OrderViewModels> Get(GetAllOrderByUser request);
    }
}
