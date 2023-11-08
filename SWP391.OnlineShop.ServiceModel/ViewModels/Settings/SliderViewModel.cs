using Microsoft.AspNetCore.Http;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Settings
{
    public class SliderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile ThumbnailFile { get; set; }
        public string Image { get; set; }
        public string BlackLink { get; set; }
        public Status Status { get; set; }
    }
}
