using Microsoft.AspNetCore.Http;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(200, ErrorMessage = "Product Name is no longer than 200 characters")]
        public string ProductName { get; set; }
        public IFormFile ThumbnailFile { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0, 1000, ErrorMessage = "Amount must greater than 0 and less than 1000")]
        public int Amount { get; set; }
        public List<ProductSizeViewModel> ProductSizes { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Sale Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Sale Price must greater than 0")]
        public decimal SalePrice { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<FeedbackViewModels> FeedBacks { get; set; }
    }
}
