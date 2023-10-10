using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Category
{
	public class CategoryViewModel
	{
		public int Id { get; set; }
		public Status Status { get; set; }
		public string? CategoryName { get; set; }
		public CategoryType CategoryType { get; set; }
		//public ICollection<Post>? Posts { get; set; }
		public int TotalProduct { get; set; }
  }
}
