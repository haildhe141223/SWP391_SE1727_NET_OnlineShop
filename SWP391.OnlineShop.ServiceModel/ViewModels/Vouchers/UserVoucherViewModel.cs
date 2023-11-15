using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers
{
    public class UserVoucherViewModel
    {
        public int Id { get; set; }
        public string VoucherName { get; set; }
        public string VoucherCode { get; set; }
        public VoucherType Type { get; set; }
        public decimal Value { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
