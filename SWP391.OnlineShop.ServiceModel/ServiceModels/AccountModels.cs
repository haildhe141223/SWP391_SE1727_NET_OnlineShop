using ServiceStack;
using SWP391.OnlineShop.ServiceModel.Results;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels;

public class AccountModels
{
    [Route("/Account/GetUser", "GET")]
    public class GetUser : IReturn<BaseResultModel>
    {
        public string? Email { get; set; }
        public string? ProviderKey { get; set; }
    }
}