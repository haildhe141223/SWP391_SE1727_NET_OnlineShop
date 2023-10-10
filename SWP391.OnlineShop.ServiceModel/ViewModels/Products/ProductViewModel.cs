﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Category;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		public Status Status { get; set; }
		public int? CategoryId { get; set; }
		public string? ProductName { get; set; }
		public string? Thumbnail { get; set; }
		public string? Description { get; set; }
		public int Amount { get; set; }
		public decimal Price { get; set; }
		public decimal SalePrice { get; set; }
		public CategoryViewModel? Category { get; set; }
		public List<FeedbackViewModel>? FeedBacks { get; set; }

	}
}
