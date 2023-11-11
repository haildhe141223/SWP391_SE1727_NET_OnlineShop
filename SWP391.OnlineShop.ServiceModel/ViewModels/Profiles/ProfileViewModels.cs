using SWP391.OnlineShop.Common.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;

public class ProfileViewModels
{
    public GeneralViewModel GeneralViewModel { get; set; }
    public SecurityViewModel SecurityViewModel { get; set; }
    public AddressViewModels AddressViewModel { get; set; }
    public ProfileTab RequestTab { get; set; }
}