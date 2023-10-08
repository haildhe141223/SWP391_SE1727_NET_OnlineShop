using SWP391.OnlineShop.ServiceModel.Results;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Address
{
	public class AddressViewModel : BaseResultModel
	{
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string FullAddress { get; set; }
        public string UserEmail { get; set; }
        public int UserId { get; set; }
        public string ProvinceName { get; set; }
        public string WardName { get; set; }
        public string DistrictName { get; set; }
    }
    public class ProvinceViewModel : BaseResultModel
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; }
    }

	public class DistrictViewModel : BaseResultModel
	{
		public int Id { get; set; }
		public string DistrictName { get; set; }
	}

	public class WardViewModel : BaseResultModel
	{
		public int Id { get; set; }
		public string WardName { get; set; }
	}
}
