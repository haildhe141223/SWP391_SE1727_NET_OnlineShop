using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Accounts;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email cannot be empty")]
    [MaxLength(256, ErrorMessage = "Email max-length is 256 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; }

    public bool IsPersistent { get; set; }
}