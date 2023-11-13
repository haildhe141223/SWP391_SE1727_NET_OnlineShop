using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class ProductSizeViewModel
    {
		public int ProductId { get; set; }
		public int SizeId { get; set; }
		public int Quantity { get; set; }

		public SizeViewModel Size { get; set; }
	}
}
