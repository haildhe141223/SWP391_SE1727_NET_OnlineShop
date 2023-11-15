using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Requests;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels;

public class RequestModels
{
    [Route("/Request/GetRequests", "GET")]
    public class GetRequests : IReturn<List<RequestManageViewModel>>
    {
    }

    [Route("/Request/GetProcessedRequests", "GET")]
    public class GetProcessedRequests : IReturn<List<RequestManageViewModel>>
    {
    }

    [Route("/Request/GetRequest", "GET")]
    public class GetRequest : IReturn<RequestManageViewModel>
    {
        public int RequestId { get; set; }
    }

    [Route("/Request/DeleteRequest", "DELETE")]
    public class DeleteRequest : IReturn<BaseResultModel>
    {
        public int RequestId { get; set; }
    }

    [Route("/Request/PutUpdateRequest", "PUT")]
    public class PutUpdateRequest : IReturn<BaseResultModel>
    {
        public int RequestId { get; set; }
        public string RequestReplyData { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }

    [Route("/Request/PutRestoreRequest", "PUT")]
    public class PutRestoreRequest : IReturn<BaseResultModel>
    {
        public int RequestId { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    }
}