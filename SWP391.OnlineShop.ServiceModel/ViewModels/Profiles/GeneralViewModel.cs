using SWP391.OnlineShop.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;

public class GeneralViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Please input username. This field is required")]
    public string DisplayName { get; set; }

    [Required(ErrorMessage = "Please input email. This field is required")]
    public string DisplayEmail { get; set; }

    [Required(ErrorMessage = "Please input image. This field is required")]
    public string DisplayImage { get; set; }

    [Required(ErrorMessage = "Please input gender. This field is required")]
    public Gender DisplayGender { get; set; }

    [Required(ErrorMessage = "Please input phone number. This field is required")]
    public string DisplayPhoneNumber { get; set; }
}