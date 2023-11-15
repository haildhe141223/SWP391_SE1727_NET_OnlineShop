using SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Users;

public class UserDetailViewModels
{
    public string UserUpdateId { get; set; }
    public UserPermissionViewModel PermissionViewModel { get; set; }
    public UserIdentityViewModel UserIdentityViewModel { get; set; }
    public UserSecurityViewModel UserSecurityViewModel { get; set; }
    public AddressViewModels AddressViewModels { get; set; }
}