using ServiceStack;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Address;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    public class AddressModel
    {
        [Route("/Address/GetAddressByUser", "GET")]
        public class GetAddressByUser : IReturn<AddressViewModel>
        {
            public string Email { get; set; }
        }

        [Route("/Address/GetDistrictByProvince", "GET")]
        public class GetDistrictByProvince : IReturn<List<DistrictViewModel>>
        {
            public int ProvinceId { get; set; }
        }

        [Route("/Address/GetAllProvince", "GET")]
        public class GetAllProvince : IReturn<List<ProvinceViewModel>>
        {
        }

        [Route("/Address/GetWardByDistrict", "GET")]
        public class GetWardByDistrict : IReturn<List<WardViewModel>>
        {
            public int DistrictId { get; set; }
        }

        [Route("/Address/PostAddAddress", "POST")]
        public class PostAddAddress : IReturn<AddressViewModel>
        {
        }

        [Route("/Address/PutUpdateAddress", "PUT")]
        public class PutUpdateAddress : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
            public string FullAddress { get; set; }
        }
    }
}
