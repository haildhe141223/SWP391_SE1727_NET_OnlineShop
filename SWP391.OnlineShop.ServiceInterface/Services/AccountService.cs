using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;

namespace SWP391.OnlineShop.ServiceInterface.Services;

public class AccountService : BaseService, IAccountService
{
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    public AccountService(
        ILoggerService logger,
        IMapper mapper,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        SignInManager<User> signInManager,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
    }
}