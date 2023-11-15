using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles
{
    public class RequestMarketingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Request to become Blogger/Marketer - Please provide your author nickname.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Request to become Blogger/Marketer - Please provide your email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Request to become Blogger/Marketer - Please provide your phone number.")]
        public string Phone { get; set; }

        [Display(Name = "Sample-Post Link - Please help to provide your sample post - We will based on this as result.")]
        public string SamplePostLink { get; set; }
    }
}
