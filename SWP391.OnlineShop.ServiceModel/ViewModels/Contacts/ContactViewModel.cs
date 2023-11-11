using SWP391.OnlineShop.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Contacts
{
    public class ContactViewModel
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "This email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This subject field is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "This message field is required")]
        public string Message { get; set; }
    }
}
