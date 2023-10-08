using SWP391.OnlineShop.ServiceModel.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
	public class ProductCategoryViewModel
	{
		public List<ProductViewModel>? LatestProducts { get; set; }
		public List<CategoryViewModel>? Categories { get; set; }
		public List<ProductViewModel>? ProductsOfWeek { get; set; }

	}
}
