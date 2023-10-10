using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Cart
{
    public class OrderDetailViewModel : BaseResultModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public Status Status { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
