using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Cart
{
    public class OrderDetailViewModel
    {
        public DateTime OrderDateTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public Gender CustomerGender { get; set; }
        public string CustomerEmail { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
