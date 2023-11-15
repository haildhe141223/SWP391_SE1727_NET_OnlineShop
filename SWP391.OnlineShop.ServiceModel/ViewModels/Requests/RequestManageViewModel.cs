using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Requests;

public class RequestManageViewModel
{
    public int Id { get; set; }
    public string RequestData { get; set; }
    public RequestType RequestType { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public string Reply { get; set; }
    public string User { get; set; }
    public DateTime CreatedDateTime { get; set; }
}