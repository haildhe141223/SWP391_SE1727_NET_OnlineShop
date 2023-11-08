using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    public class SliderModel
    {

        [Route("/Slider/GetAllSlider", "GET")]
        public class GetAllSlider : IReturn<List<SliderViewModel>>
        {

        }
        [Route("/Slider/GetSliderById", "GET")]
        public class GetSliderById : IReturn<SliderViewModel>
        {
            public int SliderId { get; set; }
        }
        [Route("/Slider/PostAddSlider", "POST")]
        public class PostAddSlider : IReturn<SliderViewModel>
        {
            public string Title { get; set; }
            public string Image { get; set; }
            public string BlackLink { get; set; }
            public Status Status { get; set; }
        }

        [Route("/Slider/PutUpdateSlider", "PUT")]
        public class PutUpdateSlider : IReturn<SliderViewModel>
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string BlackLink { get; set; }
            public Status Status { get; set; }
        }

        [Route("/Slider/DeleteSlider", "DELETE")]
        public class DeleteSlider : IReturn<SliderViewModel>
        {
            public int SliderId { get; set; }
        }
    }
}
