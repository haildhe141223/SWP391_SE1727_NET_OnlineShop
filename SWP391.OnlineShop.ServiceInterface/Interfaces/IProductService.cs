using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
	public interface IProductService
	{
		/// <summary>
		/// Get all products
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Products</returns>
		List<ProductViewModel> Get(GetAllProduct request);

        /// <summary>
        /// Get all active products
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Products</returns>
        List<ProductViewModel> Get(GetAllActiveProduct request);

        /// <summary>
        /// Get all coming products
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Products</returns>
        List<ProductViewModel> Get(GetAllComingProduct request);

        /// <summary>
        /// Get product by category id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Product</returns>
        List<ProductViewModel> Get(GetProductByCategoryId request);

		/// <summary>
		/// Get product by tag id
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Product</returns>
		List<ProductViewModel> Get(GetProductByTagId request);

		/// <summary>
		/// Get hot deals products
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Products has biggest deal</returns>
		List<ProductViewModel> Get(GetHotDealProduct request);

        /// <summary>
        /// Get deal product of week
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Products has biggest deal in week</returns>
        List<ProductViewModel> Get(GetDealProductOfWeek request);

		/// <summary>
		/// Get product By Id
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Product</returns>
		ProductViewModel Get(GetProductById request);

        /// <summary>
        /// Get Product By Id and SizeId
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Product</returns>
        ProductSizeViewModel Get(GetProductByIdAndSizeId request);

		/// <summary>
		/// Get product's feedback by id
		/// </summary>
		/// <param name="request"></param>
		/// <returns>product feedbacks</returns>
		ProductViewModel Get(GetProductFeedbackById request);

		/// <summary>
		/// Post Add Product
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Product</returns>
		Task<BaseResultModel> Post(PostAddProduct request);

		/// <summary>
		/// Delete Product
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		Task<BaseResultModel> Delete(DeleteProduct request);

		/// <summary>
		/// Update Product
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Product</returns>
		Task<BaseResultModel> Put(PutUpdateProduct request);

		/// <summary>
		/// Add new Comment
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		void Post(Comment request);

        /// <summary>
        /// Get product of voucher
        /// </summary>
        /// <param name="request"></param>
        /// <returns>product of voucher</returns>
        List<OrderViewModels> Get(GetOrderWithVoucher request);

		/// <summary>
		/// Update Product
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Product</returns>
		Task<BaseResultModel> Put(PutUpdateProductSize request);
	}
}

