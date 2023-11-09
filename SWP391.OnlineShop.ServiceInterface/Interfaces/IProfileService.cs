using SWP391.OnlineShop.ServiceModel.Results;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ProfileModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces;

public interface IProfileService
{
    Task<BaseResultModel> Put(PutUpdateUserName request);
    Task<BaseResultModel> Put(PutUpdateUserEmail request);
    Task<BaseResultModel> Put(PutUpdateUserAvatar request);
    Task<BaseResultModel> Put(PutUpdateUserPhone request);
    Task<BaseResultModel> Put(PutUpdateUserGender request);
}