using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;

public class SecurityViewModel
{
    [Required(ErrorMessage = "Please input current password. This field is required")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Please input new password. This field is required")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Please input retype password. This field is required")]
    [StringLength(50, ErrorMessage = "Must be between 8 and 50 characters", MinimumLength = 8)]
    [Compare(nameof(NewPassword))]
    public string RePassword { get; set; }
}