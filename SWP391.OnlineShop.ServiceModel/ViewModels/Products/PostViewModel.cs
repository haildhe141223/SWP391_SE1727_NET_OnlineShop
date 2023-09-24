using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class PostViewModel
    {
        public string? Title { get; set; }
        public string? Featured { get; set; }
        public string? Brief { get; set; }
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public string? Author { get; set; }
        public int? CategoryId { get; set; }

        //public Category Category { get; set; }
    }
}
