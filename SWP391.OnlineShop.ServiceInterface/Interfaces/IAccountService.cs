using SWP391.OnlineShop.ServiceModel.Results;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AccountModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces;

public interface IAccountService
{
    Task<BaseResultModel> Get(GetLogin request);
    Task<BaseResultModel> Get(GetExternalLogin request);
    Task<BaseResultModel> Get(GetUser request);
}