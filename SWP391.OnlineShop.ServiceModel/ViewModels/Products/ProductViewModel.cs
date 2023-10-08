using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{

	public class ProductViewModel
	{
		public int Id { get; set; }
		public int? CategoryId { get; set; }
		public string? ProductName { get; set; }
		public string? Thumbnail { get; set; }
		public string? Description { get; set; }
		public int Amount { get; set; }
		public decimal Price { get; set; }
		public decimal SalePrice { get; set; }
    public Status Status { get; set; }
		public SWP391.OnlineShop.Core.Models.Entities.Category? Category { get; set; }
		public ICollection<FeedBack>? FeedBacks { get; set; }

	}

}
