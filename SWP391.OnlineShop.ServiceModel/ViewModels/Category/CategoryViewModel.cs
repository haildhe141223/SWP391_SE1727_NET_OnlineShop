using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Category
{
	public class CategoryViewModel
	{
		public int Id { get; set; }
		public string? CategoryName { get; set; }
		public CategoryType CategoryType { get; set; }
		//public ICollection<Post>? Posts { get; set; }
		public List<Product>? Products { get; set; }

	}
}
