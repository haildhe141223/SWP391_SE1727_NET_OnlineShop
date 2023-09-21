using Hangfire.Dashboard;
using SWP391.OnlineShop.Common.Constraints;

namespace SWP391.OnlineShop.BatchJob.Configs.HangFire;

public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var isAuthorize = httpContext.User.Identity?.IsAuthenticated;

        if (isAuthorize is true)
        {
            isAuthorize = httpContext.User.IsInRole(RoleConstraints.Admin);
        }

        return isAuthorize ?? false;
    }

    public bool IsReadOnlyHangFireDashboard(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var isAuthorize = httpContext.User.Identity?.IsAuthenticated;
        return isAuthorize ?? false;
    }
}