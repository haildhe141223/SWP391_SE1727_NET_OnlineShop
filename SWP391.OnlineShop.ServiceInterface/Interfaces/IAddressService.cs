using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Address;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AddressModel;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
	public interface IAddressService
	{
		Task<AddressViewModel> Get(GetAddressByUser request);
		List<DistrictViewModel> Get(GetDistrictByProvince request);
		List<ProvinceViewModel> Get(GetAllProvince request);
		List<WardViewModel> Get(GetWardByDistrict request);
		Task<BaseResultModel> Put(PutUpdateAddress request);
	}
}
