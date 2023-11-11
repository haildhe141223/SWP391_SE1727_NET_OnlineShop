using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Emails;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Dashboard;

namespace SWP391.OnlineShop.ServiceInterface.Services;

public class DashboardService : BaseService, IDashboardService
{
    private readonly ILoggerService _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public DashboardService(
        ILoggerService logger,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMailService mailService)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mailService = mailService;
    }

    public async Task<DashboardViewModel> Get(DashboardModels.GetDashboard request)
    {
        var totalUsers = await _unitOfWork.Context.Users.CountAsync(x => x.LockoutEnabled == false);
        var totalPosts = await _unitOfWork.Context.Posts.CountAsync(x => x.Status == Status.Active);
        var totalOrders = await _unitOfWork.Context.Orders.CountAsync(x => x.Status == Status.Active);
        var totalProducts = await _unitOfWork.Context.Products.CountAsync(x => x.Status == Status.Active);

        var dashboardViewModel = new DashboardViewModel
        {
            TotalOrders = totalOrders,
            TotalProducts = totalProducts,
            TotalPosts = totalPosts,
            TotalUsers = totalUsers
        };

        return dashboardViewModel;
    }
}