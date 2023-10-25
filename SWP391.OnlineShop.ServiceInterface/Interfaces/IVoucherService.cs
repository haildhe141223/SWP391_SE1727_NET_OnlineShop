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
		List<VoucherViewModels> Get(GetAllVoucherByUser request);

		/// <summary>
		/// Get Voucher By Id
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Voucher Model</returns>
		VoucherViewModels Get(GetAllVoucherById request);

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
	}
}
