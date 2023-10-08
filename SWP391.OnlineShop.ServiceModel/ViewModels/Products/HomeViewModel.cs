using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
	public class HomeViewModel
	{
		public List<ProductViewModel>? LatestProducts { get; set; }
		public List<ProductViewModel>? HotDealProduct { get; set; }
		public ProductViewModel? ProductDetail { get; set; }
		public List<ProductViewModel>? ProductsOfWeek { get; set; }
	}
}
