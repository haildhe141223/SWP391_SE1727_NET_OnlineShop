using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface ISizeService
    {
        /// <summary>
		/// Get all sizes
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Products</returns>
		List<SizeViewModel> Get(GetAllSize request);

        /// <summary>
		/// Get all active sizes
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Products</returns>
		List<SizeViewModel> Get(GetAllActiveSize request);

        /// <summary>
		/// Get size by id
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Products</returns>
		SizeViewModel Get(GetSizeById request);

        /// <summary>
        /// Post Add Size
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Product</returns>
        Task<BaseResultModel> Post(PostAddSize request);

        /// <summary>
        /// Delete Size
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResultModel> Delete(DeleteSize request);

        /// <summary>
        /// Update Size
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Product</returns>
        Task<BaseResultModel> Put(PutUpdateSize request);
    }
}
