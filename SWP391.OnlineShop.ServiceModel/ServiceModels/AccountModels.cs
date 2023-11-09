using ServiceStack;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Accounts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels;

public class AccountModels
{
    [Route("/Account/GetUsers", "GET")]
    public class GetUsers : IReturn<List<UserViewModel>>
    {
        public bool IsDesc { get; set; }
        public int Size { get; set; }
    }

    [Route("/Account/GetUser", "GET")]
    public class GetUser : IReturn<List<UserViewModel>>
    {
        public string Id { get; set; }
    }

    [Route("/Account/GetExternalUser", "GET")]
    public class GetExternalUser : IReturn<BaseResultModel>
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

    [Route("/Account/RegisterAccount", "POST")]
    public class PostRegisterAccount : IReturn<BaseResultModel>
    {
        public RegisterViewModel RegisterViewModel { get; set; }
    }

    [Route("/Account/SendConfirmRegisterEmail", "POST")]
    public class PostConfirmRegisterEmail : IReturn<BaseResultModel>
    {
        public string To { get; set; }
        public string Category { get; set; }
        public string LinkConfirmPassword { get; set; }
    }

    [Route("/Account/SendConfirmChangePasswordEmail", "POST")]
    public class PostConfirmChangePasswordEmail : IReturn<BaseResultModel>
    {
        public string To { get; set; }
        public string Category { get; set; }
        public string LinkResetPassword { get; set; }
    }

    [Route("/Account/GetCustomers", "GET")]
    public class GetCustomers : IReturn<List<UserViewModel>>
    {
        public bool IsDesc { get; set; }
    }

    [Route("/Account/UpdateCustomer", "PUT")]
    public class UpdateCustomer : IReturn<BaseResultModel>
    {
        public int Id { get; set; }
        public bool LockoutEnabled { get; set; }

    [Route("/Account/DeleteUser", "DELETE")]
    public class DeleteUser : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
    }
}