using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;
using SWP391.OnlineShop.ServiceModel.ViewModels.Requests;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.RequestModels;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers;

[Authorize(Roles = RoleConstraints.Admin)]
public class RequestController : BaseController
{
    private readonly IJsonServiceClient _client;

    public RequestController(
        IJsonServiceClient client)
    {
        _client = client;
    }
    public async Task<IActionResult> Index()
    {
        var requests = await _client.GetAsync(new GetRequests()) ?? new List<RequestManageViewModel>();
        return View(requests);
    }

    public async Task<IActionResult> ProcessedRequests()
    {
        var requests = await _client.GetAsync(new GetProcessedRequests()) ?? new List<RequestManageViewModel>();
        return View(requests);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (id == 0)
        {
            TempData["ErrorMess"] = "Invalid data while gather request information - Please double check.";
            return RedirectToAction(nameof(Index));
        }

        var requestVm = await _client.GetAsync(new GetRequest
        {
            RequestId = id
        });

        if (requestVm.Id != 0)
        {
            if (requestVm.RequestType == RequestType.RequestToBecomeMarketing)
            {
                var data = JsonConvert.DeserializeObject<RequestMarketingViewModel>(requestVm.RequestData);
                if (data != null)
                {
                    data.Id = requestVm.Id;
                    return View("RequestMarketing", data);
                }
            }

            if (requestVm.RequestType == RequestType.RequestToBecomeSaleManager)
            {
                var data = JsonConvert.DeserializeObject<RequestSaleManagerViewModel>(requestVm.RequestData);

                if (data != null)
                {
                    data.Id = requestVm.Id;
                    return View("RequestSaleManager", data);
                }
            }
        }

        TempData["ErrorMess"] = "Invalid data id while gather request information - Please double check.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reply(string replyData, int replyId, string button)
    {
        if (string.IsNullOrEmpty(button) || replyId == 0)
        {
            TempData["ErrorMess"] = $"Invalid data while reply request with id [{replyId}] - Please double check.";
            return RedirectToAction(nameof(Index));
        }

        var requestStatus = button switch
        {
            RequestConstraints.Approve => RequestStatus.Approved,
            RequestConstraints.Reject => RequestStatus.Rejected,
            RequestConstraints.Pending => RequestStatus.Pending,
            _ => RequestStatus.Submitted
        };

        var isUpdateRequest = await _client.PutAsync(new PutUpdateRequest
        {
            RequestId = replyId,
            RequestStatus = requestStatus,
            RequestReplyData = replyData
        });

        if (isUpdateRequest.StatusCode != Common.Enums.StatusCode.Success)
        {
            TempData["ErrorMess"] = $"Update request with status - [{button}] fail - Please double check.";
            return RedirectToAction("Index");
        }

        TempData["SuccessMess"] = $"Update request with status - [{button}] success - Please double check.";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Restore(int id)
    {
        var isRestore = await _client.PutAsync(new PutRestoreRequest
        {
            RequestId = id
        });

        if (isRestore.StatusCode != Common.Enums.StatusCode.Success)
        {
            TempData["ErrorMess"] = $"Restore request with id - [{id}] fail - Please double check.";
            return RedirectToAction("ProcessedRequests");
        }

        TempData["SuccessMess"] = $"Restore request with id - [{id}] success - Please double check.";
        return RedirectToAction("ProcessedRequests");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var isDelete = await _client.DeleteAsync(new DeleteRequest
        {
            RequestId = id
        });

        if (isDelete.StatusCode != Common.Enums.StatusCode.Success)
        {
            TempData["ErrorMess"] = $"Delete request with id - [{id}] fail - Please double check.";
            return RedirectToAction("ProcessedRequests");
        }

        TempData["SuccessMess"] = $"Delete request with id - [{id}] success - Please double check.";
        return RedirectToAction("ProcessedRequests");
    }
}