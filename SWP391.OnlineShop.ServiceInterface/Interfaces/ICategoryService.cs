using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get all categories by type
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get all categories by type</returns>
        List<CategoryViewModel> Get(GetAllCategory request);

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get all categories</returns>
        List<CategoryViewModel> Get(GetAllCategories request);

        /// <summary>
        /// Get category by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Get category by Id</returns>
        CategoryViewModel Get(GetCategoryById request);

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResultModel> Post(PostAddCategory request);

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResultModel> Put(PutUpdateCategory request);

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResultModel> Delete(DeleteCategory request);
    }
}
