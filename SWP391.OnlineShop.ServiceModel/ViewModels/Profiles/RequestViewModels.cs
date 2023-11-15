using SWP391.OnlineShop.Common.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles
{
    public class RequestViewModels
    {
        public List<RequestDataViewModel> RequestDataViewModels { get; set; }
        public RequestMarketingViewModel RequestMarketingViewModel { get; set; }
        public RequestSaleManagerViewModel RequestSaleManagerViewModel { get; set; }
        public ProfileTab ProfileTab { get; set; } = ProfileTab.Collaboration;
    }
}
