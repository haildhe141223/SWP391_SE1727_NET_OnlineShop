using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Cart;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
	public class OrderService : BaseService, IOrderService
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

        public async Task<BaseResultModel> Delete(DeleteOrderDetail request)
        {
            var result = new BaseResultModel();
            _logger.LogInfo("Delete Order");
            try
            {
                var orderDetail =  _unitOfWork.Orders.GetOrderDetailByOrderDetailId(request.Id);
                if (orderDetail == null)
                {
                    result.ErrorMessage = "Order Detail not exist";
                    result.StatusCode = Common.Enums.StatusCode.NotFound;
                    return result;
                }
                 _unitOfWork.Orders.DeleteOrderDetail(request.Id);
                var row = await _unitOfWork.CompleteAsync();
                if (row > 0)
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
				if (row > 0)
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
		public OrderViewModel Get(GetCartDetailByUser request)
        {
            var result = new OrderViewModel();
            _logger.LogInfo("Get Cart Detail By User");
            try
            {
				var model = _unitOfWork.Orders.GetCartDetailByUser(request.Email)
					 .OrderByDescending(o => o.OrderDateTime)
					 .ToList()
					 .FirstOrDefault();
				if (model is not null)
				{
					result = _mapper.Map<OrderViewModel>(model);
				}
			}
            catch (Exception ex)
            {
                _logger.LogError($"GetCartDetailByUser error {ex.Message}");
            }
            return result;
        }

        public OrderViewModel Get(GetCartContactByUser request)
        {
            var result = new OrderViewModel();
            _logger.LogInfo("Get Cart Contact By User");
            try
            {
				var model = _unitOfWork.Orders.GetCartContactByUser(request.Email)
				   .OrderByDescending(o => o.OrderDateTime)
				   .ToList()
				   .FirstOrDefault();
				if (model is not null)
				{
					result = _mapper.Map<OrderViewModel>(model);
				}
			}
            catch (Exception ex)
            {
                _logger.LogError($"GetCartContactByUser error {ex.Message}");
            }
            return result;
        }

        public OrderViewModel Get(GetCartCompletionByUser request)
        {
            var result = new OrderViewModel();
            _logger.LogInfo("Get Cart Completion By User");
            try
            {
                var model = _unitOfWork.Orders.GetCartCompletionByUser(request.Email)
                    .OrderByDescending(o => o.OrderDateTime)
                    .ToList()
                    .FirstOrDefault();
                if(model is not null)
                {
                    result = _mapper.Map<OrderViewModel>(model);
				}
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCartCompletionByUser error {ex.Message}");
            }
            return result;
        }

		public OrderViewModel Get(GetCartInfo request)
        {
			var result = new OrderViewModel();
			_logger.LogInfo("Get Cart Completion By User");
			try
			{
                var model = _unitOfWork.Orders.GetOrderInfoById(request.Id);
				if (model is not null)
				{
					result = _mapper.Map<OrderViewModel>(model);
                }
                else
                {
                    result.ErrorMessage = "Not found";
                    result.StatusCode = StatusCode.NotFound;
                }
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetCartCompletionByUser error {ex.Message}");
			}
			return result;
		}

		public async Task<OrderViewModel> Post(PostAddToCart request)
        {
            var result = new OrderViewModel();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user == null)
                {
                    throw new Exception($"User Not Found");
                }
                var order = new Order()
                {
                    CustomerAddress = request.CustomerAddress,
                    CustomerEmail = request.CustomerEmail,
                    CustomerGender = request.CustomerGender,
                    CustomerName = request.CustomerName,
                    OrderDateTime = DateTime.Now,
                    OrderDetails = request.OrderDetails,
                    OrderStatus = request.OrderStatus,
                    TotalCost = request.TotalCost,
                };
                await _unitOfWork.Orders.AddAsync(order);
                var rows = await _unitOfWork.CompleteAsync();
                if(rows > 0)
                {
                    result = _mapper.Map<OrderViewModel>(order);
                    result.StatusCode = Common.Enums.StatusCode.Success;
                    return result;
                }
                result.StatusCode = Common.Enums.StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostAddToCart error {ex.Message}");
            }
            return result;
        }

        public async Task<OrderViewModel> Put(PutUpdateCart request)
        {
            var result = new OrderViewModel();
            try
            {
                var order = _unitOfWork.Orders.GetById(request.Id);
                if (order == null)
                {
                    result.StatusCode = StatusCode.InternalServerError;
                    return result;
                }
                order.OrderStatus = request.OrderStatus;
                order.TotalCost = request.TotalCost;
                order.OrderDetails = request.OrderDetails;
                order.CustomerName = request.CustomerName;
                order.CustomerEmail = request.CustomerEmail;
                order.CustomerGender = request.CustomerGender;
                order.CustomerAddress = request.CustomerAddress;
                _unitOfWork.Orders.Update(order);
                var rows = await _unitOfWork.CompleteAsync();
                result.StatusCode = rows > 0 ? StatusCode.Success : StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutUpdateCart error {ex.Message}");
            }
            return result;
        }

		public async Task<OrderViewModel> Put(PutUpdateCartToContact request)
        {
			var result = new OrderViewModel();
			try
			{
				var order = _unitOfWork.Orders.GetById(request.Id);
				if (order == null)
				{
					result.StatusCode = StatusCode.InternalServerError;
					return result;
				}
				order.OrderStatus = request.OrderStatus;
				order.TotalCost = request.TotalCost;
                if (!string.IsNullOrEmpty(request.Address))
                {
                    order.CustomerAddress = request.Address;
                }
				_unitOfWork.Orders.Update(order);
				var rows = await _unitOfWork.CompleteAsync();
				result.StatusCode = rows > 0 ? StatusCode.Success : StatusCode.InternalServerError;
			}
			catch (Exception ex)
			{
				_logger.LogError($"PutUpdateCartToContact error {ex.Message}");
			}
			return result;
		}
		public async Task<BaseResultModel> Put(PutUpdateQuantity request)
		{
			var result = new OrderViewModel();
			try
			{
				var orderDetail = _unitOfWork.Orders.GetOrderDetailByOrderDetailId(request.Id);

				if (orderDetail == null)
				{
					result.StatusCode = StatusCode.NotFound;
					return result;
				}
                orderDetail.Quantity = request.Quantity;
				var rows = await _unitOfWork.CompleteAsync();
				result.StatusCode = rows > 0 ? StatusCode.Success : StatusCode.InternalServerError;
			}
			catch (Exception ex)
			{
				_logger.LogError($"PutUpdateCart error {ex.Message}");
			}
			return result;
		}
		

	}
}
