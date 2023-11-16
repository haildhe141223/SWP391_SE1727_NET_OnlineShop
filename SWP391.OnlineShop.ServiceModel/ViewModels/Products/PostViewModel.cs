using Microsoft.AspNetCore.Http;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class PostViewModel
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
        public string Tag { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }
        public Status Status { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
