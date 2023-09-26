using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceModel.Results;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Cart
{
    public class OrderDetailViewModel : BaseResultModel
    {
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public Product Product { get; set; }
    }
}
