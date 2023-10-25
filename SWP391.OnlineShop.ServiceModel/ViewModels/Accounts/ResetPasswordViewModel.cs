using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Accounts;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Please enter UserID data.")]
    public string UserId { get; set; }

    [Required(ErrorMessage = "Please enter ResetToken data.")]
    public string ResetToken { get; set; }

    public string NewPassword { get; set; }

    public string RePassword { get; set; }
}