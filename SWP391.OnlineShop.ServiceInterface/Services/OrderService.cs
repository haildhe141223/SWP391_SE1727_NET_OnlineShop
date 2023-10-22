using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(UserManager<User> userManager,
            ILoggerService logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResultModel> Delete(DeleteOrderDetail request)
        {
            var result = new BaseResultModel();
            _logger.LogInfo("Delete Order");
            try
            {
                var orderDetail = _unitOfWork.Orders.GetOrderDetailByOrderDetailId(request.Id);
                if (orderDetail == null)
                {
                    result.ErrorMessage = "Order Detail not exist";
                    result.StatusCode = StatusCode.NotFound;
                    return result;
                }
                _unitOfWork.Orders.DeleteOrderDetail(request.Id);
                var row = await _unitOfWork.CompleteAsync();
                if (row > 0)
                {
                    result.StatusCode = StatusCode.Success;
                    return result;
                }
                result.StatusCode = StatusCode.InternalServerError;
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
                    result.StatusCode = StatusCode.BadRequest;
                    return result;
                }
                await _unitOfWork.Orders.DeleteAsync(order);
                var row = await _unitOfWork.CompleteAsync();
                if (row > 0)
                {
                    result.StatusCode = StatusCode.Success;
                    return result;
                }
                result.StatusCode = StatusCode.InternalServerError;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete Cart Error {ex.Message}");
            }
            return result;
        }
        public OrderViewModels Get(GetCartDetailByUser request)
        {
            var result = new OrderViewModels();
            _logger.LogInfo("Get Cart Detail By User");
            try
            {
                var model = _unitOfWork.Orders.GetCartDetailByUser(request.Email)
                     .OrderByDescending(o => o.OrderDateTime)
                     .ToList()
                     .FirstOrDefault();
                if (model is not null)
                {
                    result = _mapper.Map<OrderViewModels>(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCartDetailByUser error {ex.Message}");
            }
            return result;
        }

        public OrderViewModels Get(GetCartContactByUser request)
        {
            var result = new OrderViewModels();
            _logger.LogInfo("Get Cart Contact By User");
            try
            {
                var model = _unitOfWork.Orders.GetCartContactByUser(request.Email)
                   .OrderByDescending(o => o.OrderDateTime)
                   .ToList()
                   .FirstOrDefault();
                if (model is not null)
                {
                    result = _mapper.Map<OrderViewModels>(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCartContactByUser error {ex.Message}");
            }
            return result;
        }

        public OrderViewModels Get(GetCartCompletionByUser request)
        {
            var result = new OrderViewModels();
            _logger.LogInfo("Get Cart Completion By User");
            try
            {
                var model = _unitOfWork.Orders.GetCartCompletionByUser(request.Email)
                    .OrderByDescending(o => o.OrderDateTime)
                    .ToList()
                    .FirstOrDefault();
                if (model is not null)
                {
                    result = _mapper.Map<OrderViewModels>(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCartCompletionByUser error {ex.Message}");
            }
            return result;
        }

        public OrderViewModels Get(GetCartInfo request)
        {
            var result = new OrderViewModels();
            _logger.LogInfo("Get Cart Completion By User");
            try
            {
                var model = _unitOfWork.Orders.GetOrderInfoById(request.Id);
                if (model is not null)
                {
                    result = _mapper.Map<OrderViewModels>(model);
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

        public List<OrderViewModels> Get(GetAllOrderByUser request)
        {
            var result = new List<OrderViewModels>();
            _logger.LogInfo("GetAllOrderByUser");
            try
            {
                var listModel = _unitOfWork.Orders.GetOrdersByUser(request.Email)
                     .OrderByDescending(o => o.OrderDateTime)
                     .ToList();
                foreach (var item in listModel)
                {
                    var order = _mapper.Map<OrderViewModels>(item);
                    result.Add(order);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllOrderByUser error {ex.Message}");
            }
            return result;
        }

        public async Task<OrderViewModels> Post(PostAddToCart request)
        {
            var result = new OrderViewModels();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.CustomerEmail);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                var orderInCartDetail = _unitOfWork.Context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product).Where(o =>
                o.OrderStatus == Core.Models.Enums.OrderStatus.InCartDetail
                && o.Status == Core.Models.Enums.Status.Active).OrderByDescending(o => o.OrderDateTime).FirstOrDefault();
                if (orderInCartDetail is null)
                {
                    var order = new Order()
                    {
                        CustomerAddress = request.CustomerAddress,
                        CustomerEmail = request.CustomerEmail,
                        CustomerGender = request.CustomerGender,
                        CustomerName = request.CustomerName,
                        OrderDateTime = DateTime.Now,
                        OrderStatus = request.OrderStatus,
                        TotalCost = request.Quantity * request.Price,
                        CustomerId = user.Id
                    };

                    await _unitOfWork.Orders.AddAsync(order);
                    var rows = await _unitOfWork.CompleteAsync();
                    if (rows > 0)
                    {
                        result = _mapper.Map<OrderViewModels>(order);
                        var orderDetail = new OrderDetail()
                        {
                            ProductId = request.ProductId,
                            UnitPrice = request.Price,
                            Quantity = request.Quantity,
                            OrderId = result.Id
                        };
                        await _unitOfWork.Context.OrderDetails.AddAsync(orderDetail);
                        await _unitOfWork.CompleteAsync();
                        result.StatusCode = StatusCode.Success;
                        return result;
                    }
                }
                else
                {
                    var quantity = 0;
                    var isProductExist = false;
                    foreach (var item in orderInCartDetail.OrderDetails)
                    {
                        if (item.ProductId == request.ProductId && item.Status == Core.Models.Enums.Status.Active)
                        {
                            isProductExist = true;
                            quantity = item.Quantity + request.Quantity;
                            orderInCartDetail.TotalCost += request.Price * request.Quantity;

                            break;
                        }
                    }
                    _unitOfWork.Orders.Update(orderInCartDetail);
                    if (isProductExist)
                    {
                        var orderDetail = orderInCartDetail.OrderDetails.FirstOrDefault(o => o.ProductId == request.ProductId);
                        if (orderDetail != null)
                        {
                            orderDetail.Quantity = quantity;
                            _unitOfWork.Context.OrderDetails.Update(orderDetail);
                            await _unitOfWork.CompleteAsync();

                        }
                    }
                    else
                    {
                        var orderDetail = new OrderDetail()
                        {
                            ProductId = request.ProductId,
                            UnitPrice = request.Price,
                            Quantity = request.Quantity,
                            OrderId = orderInCartDetail.Id
                        };
                        await _unitOfWork.Context.OrderDetails.AddAsync(orderDetail);
                        await _unitOfWork.CompleteAsync();
                    }

                    result.StatusCode = StatusCode.Success;
                    return result;
                }


                result.StatusCode = StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostAddToCart error {ex.Message}");
            }
            return result;
        }

        public async Task<OrderViewModels> Put(PutUpdateCart request)
        {
            var result = new OrderViewModels();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(request.Id);
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

        public async Task<OrderViewModels> Put(PutUpdateCartToContact request)
        {
            var result = new OrderViewModels();
            try
            {
                var order = _unitOfWork.Orders.GetOrderInfoById(request.Id);
                if (order == null)
                {
                    result.StatusCode = StatusCode.InternalServerError;
                    return result;
                }
                order.OrderStatus = request.OrderStatus;
                if (request.TotalCost > 0)
                {
                    order.TotalCost = request.TotalCost;
                }
                if (!string.IsNullOrEmpty(request.Address))
                {
                    order.CustomerAddress = request.Address;
                }
                if (!string.IsNullOrEmpty(request.OrderNotes))
                {
                    order.OrderNotes = request.OrderNotes;
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
            var result = new OrderViewModels();
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

        public async Task<BaseResultModel> Put(PutUpdateCartStatus request)
        {
            var result = new OrderViewModels();
            try
            {
                var order = _unitOfWork.Orders.GetOrderInfoById(request.Id);
                if (order == null)
                {
                    result.StatusCode = StatusCode.InternalServerError;
                    return result;
                }
                order.OrderStatus = request.OrderStatus;
                _unitOfWork.Orders.Update(order);
                var rows = await _unitOfWork.CompleteAsync();
                result.StatusCode = rows > 0 ? StatusCode.Success : StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutUpdateCartStatus error {ex.Message}");
            }
            return result;
        }

        public async Task<BaseResultModel> Put(PutUpdateOrderNotes request)
        {
            var result = new OrderViewModels();
            try
            {
                var order = _unitOfWork.Orders.GetOrderInfoById(request.Id);
                if (order == null)
                {
                    result.StatusCode = StatusCode.InternalServerError;
                    return result;
                }
                order.OrderNotes = request.OrderNotes;
                _unitOfWork.Orders.Update(order);
                var rows = await _unitOfWork.CompleteAsync();
                result.StatusCode = rows > 0 ? StatusCode.Success : StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutUpdateOrderNotes error {ex.Message}");
            }
            return result;
        }
    }
}
