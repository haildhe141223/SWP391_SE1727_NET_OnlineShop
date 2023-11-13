using SWP391.OnlineShop.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles
{
    public class AddressViewModels
    {
        [Required(ErrorMessage = "Please input fullname. This field is required")]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "Invalid data. Please input phone number again")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please input address. This field is required")]
        public string Address { get; set; }

        public bool IsDefault { get; set; }

        public List<AddressViewModel> AddressViewModelList { get; set; }

        public ProfileTab ProfileTab { get; set; } = ProfileTab.Address;
    }
}
