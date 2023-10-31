using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Accounts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers
{
    public class VoucherViewModels : BaseResultModel
	{
        public int Id { get; set; }
		[Required(ErrorMessage ="Voucher Name is required")]
        public string VoucherName { get; set; }
        [Required(ErrorMessage = "Voucher Code is required")]
        public string VoucherCode { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public int Amount { get; set; }
		public int CreatedBy { get; set; }
		public UserViewModel User { get; set; }
        [Required(ErrorMessage = "Voucher Start Valid Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDateTime { get; set; }
        [Required(ErrorMessage = "Voucher Expired Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDateTime { get; set; }
        [Required(ErrorMessage = "Voucher Type is required")]
        public VoucherType Type { get; set; }
        [Required(ErrorMessage = "Voucher Value is required")]
        public decimal Value { get; set; }
        public List<int> ProductIds { get; set; }
        public List<ProductVoucherViewModel> ProductVouchers { get; set; }
    }
}
