﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class ProductViewModel
    {
        public string? ProductName { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int? CategoryId { get; set; }

        //public Category Category { get; set; }

    }
}