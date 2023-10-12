using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Featured { get; set; }
        public string Brief { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Author { get; set; }
        public int? CategoryId { get; set; }
        public Status Status { get; set; }
    }
}
