using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Requests;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.RequestModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces;

public interface IRequestService
{
    Task<List<RequestManageViewModel>> Get(GetRequests request);
    Task<List<RequestManageViewModel>> Get(GetProcessedRequests request);

    Task<RequestManageViewModel> Get(GetRequest request);

    Task<BaseResultModel> Delete(DeleteRequest request);

    Task<BaseResultModel> Put(PutUpdateRequest request);
    Task<BaseResultModel> Put(PutRestoreRequest request);
}