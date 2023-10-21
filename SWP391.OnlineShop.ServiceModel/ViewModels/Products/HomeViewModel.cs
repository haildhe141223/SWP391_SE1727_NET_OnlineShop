using SWP391.OnlineShop.ServiceModel.ViewModels.Category;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
	public class HomeViewModel
	{
		public List<ProductViewModel> LatestProducts { get; set; }
		public List<ProductViewModel> HotDealProduct { get; set; }
		public ProductViewModel ProductDetail { get; set; }
		public List<ProductViewModel> ProductsOfWeek { get; set; }
		public List<CategoryViewModel> Categories { get; set; }
	}
}
