using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Cart;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDetailViewModel>> Get(GetCartDetailByUser request);
        Task<List<OrderDetailViewModel>> Get(GetCartContactByUser request);
        Task<List<OrderDetailViewModel>> Get(GetCartCompletionByUser request);
        Task<List<OrderDetailViewModel>> Post(PostAddToCart request);
        Task<List<OrderDetailViewModel>> Put(PutUpdateCart request);
        Task<BaseResultModel> Delete(DeleteCart request);
    }
}
