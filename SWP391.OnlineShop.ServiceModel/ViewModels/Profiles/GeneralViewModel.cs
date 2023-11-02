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
}