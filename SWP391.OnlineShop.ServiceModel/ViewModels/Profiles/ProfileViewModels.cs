using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;

public class ProfileViewModels
{
    public GeneralViewModel GeneralViewModel { get; set; }
    public SecurityViewModel SecurityViewModel { get; set; }
    public AddressViewModels AddressViewModel { get; set; }
    public RequestViewModels RequestViewModel { get; set; }
    public List<UserVoucherViewModel> VoucherViewModels { get; set; }

    public ProfileTab RequestTab { get; set; }
}