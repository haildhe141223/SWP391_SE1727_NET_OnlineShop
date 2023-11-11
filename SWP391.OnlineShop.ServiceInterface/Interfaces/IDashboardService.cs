using SWP391.OnlineShop.ServiceModel.ViewModels.Dashboard;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.DashboardModels;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> Get(GetDashboard request);
}