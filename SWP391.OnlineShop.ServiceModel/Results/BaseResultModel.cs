using SWP391.OnlineShop.Common.Enums;

namespace SWP391.OnlineShop.ServiceModel.Results;

public class BaseResultModel
{
    public StatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public string SuccessMessage { get; set; }
}