using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Cart;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IOrderService
    {
        OrderViewModel Get(GetCartDetailByUser request);
        OrderViewModel Get(GetCartContactByUser request);
        OrderViewModel Get(GetCartCompletionByUser request);
        Task<OrderViewModel> Post(PostAddToCart request);
        Task<OrderViewModel> Put(PutUpdateCart request);
        Task<BaseResultModel> Put(PutUpdateQuantity request);
        Task<BaseResultModel> Delete(DeleteCart request);
		Task<BaseResultModel> Delete(DeleteOrderDetail request);
	}
}
