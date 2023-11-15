using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface ITagService
    {
        /// <summary>
		/// Get all products
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Products</returns>
		List<TagViewModel> Get(GetAllProductTag request);
    }
}
