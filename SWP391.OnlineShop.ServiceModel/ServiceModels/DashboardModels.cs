using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Dashboard;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels;

public class DashboardModels
{
    [Route("/Dashboard/GetDashboard", "GET")]
    public class GetDashboard : IReturn<DashboardViewModel>
    {
    }
}