using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get all categories</returns>
        List<CategoryViewModel> Get(GetAllCategory request);
    }
}
