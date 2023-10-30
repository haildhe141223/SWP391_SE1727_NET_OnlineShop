using SWP391.OnlineShop.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Category Type is required")]
        public CategoryType CategoryType { get; set; }
        public int TotalProduct { get; set; }
        public string Thumbnail { get; set; }
    }
}
