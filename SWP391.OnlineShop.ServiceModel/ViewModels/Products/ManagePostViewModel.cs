using Microsoft.AspNetCore.Http;
using SWP391.OnlineShop.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class ManagePostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Post Title is required")]
        [MaxLength(300, ErrorMessage = "Post Title is no longer than 300 characters")]
        public string Title { get; set; }

        public string Featured { get; set; }
        public string Brief { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public IFormFile ThumbnailFile { get; set; }
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(200, ErrorMessage = "Author is no longer than 200 characters")]
        public string Author { get; set; }
        public int? CategoryId { get; set; }
        public Status Status { get; set; }
    }
}
