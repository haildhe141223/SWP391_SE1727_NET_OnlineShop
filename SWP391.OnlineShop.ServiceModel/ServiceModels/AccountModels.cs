using ServiceStack;
using SWP391.OnlineShop.ServiceModel.Results;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels;

public class AccountModels
{
    [Route("/Account/GetUser", "GET")]
    public class GetUser : IReturn<BaseResultModel>
    {
        public string Email { get; set; }
        public string ProviderKey { get; set; }
    }

    [Route("/Account/GetLogin", "GET")]
    public class GetLogin : IReturn<BaseResultModel>
    {
        public string Email { get; set; }
    }

    [Route("/Account/ExternalLogin", "GET")]
    public class GetExternalLogin : IReturn<BaseResultModel>
    {
        public string ExternalEmail { get; set; }
        public string Username { get; set; }
        public string ProviderDisplayName { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}