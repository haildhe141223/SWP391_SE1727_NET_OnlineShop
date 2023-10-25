using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Accounts;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username cannot be empty")]
    [MaxLength(256, ErrorMessage = "Username max-length is 256 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email cannot be empty")]
    [MaxLength(256, ErrorMessage = "Email max-length is 256 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Re-password cannot be empty")]
    [StringLength(50, ErrorMessage = "Must be between 8 and 50 characters", MinimumLength = 8)]
    [Compare(nameof(Password))]
    public string RePassword { get; set; }
}