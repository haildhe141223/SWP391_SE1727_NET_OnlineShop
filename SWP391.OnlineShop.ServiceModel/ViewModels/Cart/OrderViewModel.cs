﻿using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Cart
{
    public class OrderViewModel : BaseResultModel
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public Gender CustomerGender { get; set; }
        public string? CustomerEmail { get; set; }
        public decimal TotalCost { get; set; }
        public OrderStatus OrderStatus { get; set; }
		public Status Status { get; set; }
		public ICollection<OrderDetailViewModel>? OrderDetails { get; set; }
    }
}
