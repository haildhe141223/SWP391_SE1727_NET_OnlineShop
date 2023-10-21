using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Customer
{
    public class SettingViewModel
    {

        public string? Type { get; set; }
        public decimal Value { get; set; }
        public int? VoucherId { get; set; }
        public string? Status { get; set; }
    }
}
