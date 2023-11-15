using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles
{
    public class RequestDataViewModel
    {
        public int Id { get; set; }
        public RequestType RequestType { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Reply { get; set; }
    }
}
