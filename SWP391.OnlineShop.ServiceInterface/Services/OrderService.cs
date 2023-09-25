using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Cart;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class OrderService  : BaseService, IOrderService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(UserManager<User> userManager,
            ILoggerService logger,
            IMapper mapper,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResultModel> Delete(DeleteCart request)
        {
            var result = new BaseResultModel();
            _logger.LogInfo("Delete Order");
            try
            {
                var order = await _unitOfWork.Orders.FindAsync(request.Id);
                if (order != null)
                {
                    result.ErrorMessage = "Order not exist";
                    result.StatusCode = Common.Enums.StatusCode.BadRequest;
                    return result;
                }
                await _unitOfWork.Orders.DeleteAsync(order);
                var row = await _unitOfWork.CompleteAsync();
                if(row > 0)
                {
                    result.StatusCode = Common.Enums.StatusCode.Success;
                    return result;
                }
                result.StatusCode = Common.Enums.StatusCode.InternalServerError;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete Cart Error {ex.Message}");
            }
            return result;
        }

        public async Task<List<OrderDetailViewModel>> Get(GetCartDetailByUser request)
        {
            var result = new List<OrderDetailViewModel>();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCartDetailByUser error {ex.Message}");
            }
            return result;
        }

        public async Task<List<OrderDetailViewModel>> Get(GetCartContactByUser request)
        {
            var result = new List<OrderDetailViewModel>();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCartContactByUser error {ex.Message}");
            }
            return result;
        }

        public async Task<List<OrderDetailViewModel>> Get(GetCartCompletionByUser request)
        {
            var result = new List<OrderDetailViewModel>();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCartCompletionByUser error {ex.Message}");
            }
            return result;
        }

        public async Task<List<OrderDetailViewModel>> Post(PostAddToCart request)
        {
            var result = new List<OrderDetailViewModel>();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError($"PostAddToCart error {ex.Message}");
            }
            return result;
        }

        public async Task<List<OrderDetailViewModel>> Put(PutUpdateCart request)
        {
            var result = new List<OrderDetailViewModel>();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError($"PutUpdateCart error {ex.Message}");
            }
            return result;
        }
    }
}
