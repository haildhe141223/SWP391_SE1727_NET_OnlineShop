using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Accounts;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers
{
	public class VoucherViewModels : BaseResultModel
	{
		public string VoucherName { get; set; }
		public string VoucherCode { get; set; }
		public string Amount { get; set; }
		public int CreatedBy { get; set; }
		public AccountViewModel User { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
        public VoucherType Type { get; set; }
        public decimal Value { get; set; }
    }
}
