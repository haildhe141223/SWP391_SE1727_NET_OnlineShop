using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.VoucherModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IVoucherService
    {
        /// <summary>
        /// Get Voucher By User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List Voucher Model</returns>
        Task<List<VoucherViewModels>> Get(GetAllVoucherByUser request);

        /// <summary>
        /// Get Voucher By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Voucher Model</returns>
        VoucherViewModels Get(GetVoucherById request);

        /// <summary>
        /// Add Voucher
        /// </summary>
        /// <param name="request"></param>
        /// <returns>API Status Code</returns>
        Task<BaseResultModel> Post(PostAddVoucher request);

        /// <summary>
        /// Update Voucher
        /// </summary>
        /// <param name="request"></param>
        /// <returns>API Status Code</returns>
        Task<BaseResultModel> Put(PutUpdateVoucher request);

        /// <summary>
        /// Delete Voucher
        /// </summary>
        /// <param name="request"></param>
        /// <returns>API Status Code</returns>
        Task<BaseResultModel> Delete(DeleteVoucher request);

		/// <summary>
		/// Get Voucher
		/// </summary>
		/// <param name="request"></param>
		/// <returns>List Voucher Model</returns>
		List<VoucherViewModels> Get(GetAllAvailableVoucher request);

		/// <summary>
		/// Add Voucher For User
		/// </summary>
		/// <param name="request"></param>
		/// <returns>API Status Code</returns>
		Task<BaseResultModel> Post(PostAddVoucherToUser request);

        /// <summary>
        /// Update Voucher Amount
        /// </summary>
        /// <param name="request"></param>
        /// <returns>API Status Code</returns>
        Task<BaseResultModel> Put(PutUpdateVoucherAmount request);

        /// <summary>
		/// Add Voucher To Order
		/// </summary>
		/// <param name="request"></param>
		/// <returns>API Status Code</returns>
		Task<BaseResultModel> Post(PostAddVoucherToOrder request);

		/// <summary>
		/// Delete Voucher of User
		/// </summary>
		/// <param name="request"></param>
		/// <returns>API Status Code</returns>
		Task<BaseResultModel> Delete(DeleteUserVoucher request);
	}
}
