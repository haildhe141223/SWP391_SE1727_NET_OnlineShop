using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Addresses;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Carts
{
    public class OrderViewModels : BaseResultModel
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public Gender CustomerGender { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public decimal TotalCost { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Status Status { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public ICollection<OrderDetailViewModels> OrderDetails { get; set; }
        public ICollection<ProvinceViewModel> Provinces { get; set; }
        public ICollection<ProductViewModel> Sliders { get; set; }
    }
}
