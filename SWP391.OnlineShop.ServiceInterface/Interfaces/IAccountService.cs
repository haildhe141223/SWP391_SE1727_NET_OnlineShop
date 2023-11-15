using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AccountModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces;

public interface IAccountService
{
    Task<List<UserViewModel>> Get(GetUsers request);
    Task<UserViewModel> Get(GetUser request);
    Task<List<UserViewModel>> Get(GetCustomers request);

    Task<BaseResultModel> Get(GetExternalUser request);
    Task<BaseResultModel> Get(GetLogin request);
    Task<BaseResultModel> Get(GetExternalLogin request);

    Task<UserPermissionViewModel> Get(GetUserInPermissionTab request);

    Task<BaseResultModel> Post(PostRegisterAccount request);
    Task<BaseResultModel> Post(PostConfirmRegisterEmail request);
    Task<BaseResultModel> Post(PostConfirmChangePasswordEmail request);

    Task<BaseResultModel> Put(UpdateCustomer request);
    Task<BaseResultModel> Put(PutUnlockUser request);
    Task<BaseResultModel> Put(PutUserInPermissionTab request);

    Task<BaseResultModel> Delete(DeleteUser request);
}