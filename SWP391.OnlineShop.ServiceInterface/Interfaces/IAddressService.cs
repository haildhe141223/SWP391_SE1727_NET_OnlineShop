using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Addresses;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AddressModel;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
    public interface IAddressService
    {
        /// <summary>
        /// Get Address By User Email
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Address Of User</returns>
        Task<AddressViewModel> Get(GetAddressByUser request);

        /// <summary>
        /// Get District By Province
        /// </summary>
        /// <param name="request"></param>
        /// <returns>All District of input Province</returns>
        List<DistrictViewModel> Get(GetDistrictByProvince request);

        /// <summary>
        /// Get All Province
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of Province</returns>
        List<ProvinceViewModel> Get(GetAllProvince request);

        /// <summary>
        ///  Get Ward By District
        /// </summary>
        /// <param name="request"></param>
        /// <returns>All Ward of that District</returns>
        List<WardViewModel> Get(GetWardByDistrict request);

        /// <summary>
        /// Update Address
        /// </summary>
        /// <param name="request"></param>
        /// <returns>API Status Code</returns>
        Task<BaseResultModel> Put(PutUpdateAddress request);
    }
}
