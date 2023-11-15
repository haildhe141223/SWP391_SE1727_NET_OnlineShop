using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles
{
    public class RequestSaleManagerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Request to become Sale Manager - Please enter name field.")]
        public string Name { get; set; }

        [Phone(ErrorMessage = "Request to become Sale Manager - Invalid phone input.")]
        [Required(ErrorMessage = "Request to become Sale Manager - Please enter phone field.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Request to become Sale Manager - Invalid email input.")]
        [Required(ErrorMessage = "Request to become Sale Manager - Please enter email field.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Request to become Sale Manager - Please enter address field.")]
        public string FullAddress { get; set; }

        [Display(Name = "Business Registration Certificate")]
        [Required(ErrorMessage = "Request to become Sale Manager - Please provide Business Registration Certificate.")]
        public IFormFile BusinessRegistrationCertificate { get; set; }
        public string BusinessRegistrationCertificateImage { get; set; }

        [Display(Name = "Front of IdentityCard")]
        [Required(ErrorMessage = "Request to become Sale Manager - Please provide Front of IdentityCard.")]
        public IFormFile FrontOfIdentityCard { get; set; }
        public string FrontOfIdentityCardImage { get; set; }

        [Display(Name = "Back of IdentityCard")]
        [Required(ErrorMessage = "Request to become Sale Manager - Please provide Back of IdentityCard.")]
        public IFormFile BackOfIdentityCard { get; set; }
        public string BackOfIdentityCardImage { get; set; }

        public string Reason { get; set; }
    }
}
