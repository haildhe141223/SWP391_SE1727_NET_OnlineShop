using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    //TODO: PhuongNL logger should have key to double check in log. Check AccountService for example
    public class ProductService : BaseService, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;

        public ProductService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILoggerService logger,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<BaseResultModel> Delete(DeleteProduct request)
        {
            var result = new BaseResultModel();
            try
            {
                _unitOfWork.Products.Delete(request.ProductId, request.IsHardDelete);
                var rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    result.StatusCode = Common.Enums.StatusCode.Success;
                    return result;
                }
                result.StatusCode = Common.Enums.StatusCode.InternalServerError;
                result.ErrorMessage = "Error";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<ProductViewModel> Get(GetAllProduct request)
        {
            var result = new List<ProductViewModel>();
            try
            {
                var product = _unitOfWork.Products.GetAll()
                    .OrderByDescending(x => x.CreatedDateTime).ToList();
                result = _mapper.Map<List<ProductViewModel>>(product);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<ProductViewModel> Get(GetAllActiveProduct request)
        {
            var result = new List<ProductViewModel>();
            try
            {
                var product = _unitOfWork.Products.GetAll()
                    .Where(x => x.Status == Core.Models.Enums.Status.Active)
                    .OrderByDescending(x => x.CreatedDateTime).ToList();
                result = _mapper.Map<List<ProductViewModel>>(product);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<ProductViewModel> Get(GetHotDealProduct request)
        {
            var result = new List<ProductViewModel>();
            try
            {
                var products = _unitOfWork.Products.GetHotDealProduct();
                result = _mapper.Map<List<ProductViewModel>>(products);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<ProductViewModel> Get(GetProductByCategoryId request)
        {
            var result = new List<ProductViewModel>();
            try
            {
                var product = _unitOfWork.Products.GetProductByCategoryId(Convert.ToInt32(request.CategoryId));
                result = _mapper.Map<List<ProductViewModel>>(product);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<ProductViewModel> Get(GetDealProductOfWeek request)
        {
            var result = new List<ProductViewModel>();
            try
            {
                var products = _unitOfWork.Products.GetDealProductOfWeek();
                foreach (var product in products)
                {
                    var procustVm = _mapper.Map<ProductViewModel>(product);
                    result.Add(procustVm);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public ProductViewModel Get(GetProductById request)
        {
            var result = new ProductViewModel();
            try
            {
                var product = _unitOfWork.Products.GetProductFeedbackById(request.ProductId);
                if (product != null)
                {
                    result = _mapper.Map<ProductViewModel>(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public ProductViewModel Get(GetProductFeedbackById request)
        {
            var result = new ProductViewModel();
            try
            {
                var product = _unitOfWork.Products.GetProductFeedbackById(request.ProductId);
                if (product != null)
                {
                    result = _mapper.Map<ProductViewModel>(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<OrderViewModels> Get(GetOrderWithVoucher request)
        {
            try
            {
                var query = from o in _unitOfWork.Context.Orders
                            join pv in _unitOfWork.Context.OrderVouchers
                            on o.Id equals pv.OrderId
                            join v in _unitOfWork.Context.Vouchers
                            on pv.VoucherId equals v.Id
                            where v.Id == request.VoucherId
                            select o;
                var result = query.ToList();
                if (result.Any())
                {
                    return _mapper.Map<List<OrderViewModels>>(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrderWithVoucher error {ex.Message}");
            }
            return new List<OrderViewModels>();
        }

        public ProductSizeViewModel Get(GetProductByIdAndSizeId request)
        {
            var result = new ProductSizeViewModel();
            try
            {
                var query = from p in _unitOfWork.Context.Products
                            join ps in _unitOfWork.Context.ProductSizes
                            on p.Id equals ps.ProductId
                            where ps.ProductId == request.ProductId
                            && ps.SizeId == request.SizeId
                            select new ProductSizeViewModel()
                            {
                                ProductId = ps.ProductId,
                                SizeId = ps.SizeId,
                                Quantity = ps.Quantity,
                            };
                if (query != null)
                {
                    return query.First();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProductByIdAndSizeId error {ex.Message}");
            }
            return result;
        }

        public async Task<BaseResultModel> Post(PostAddProduct request)
        {
            var result = new BaseResultModel();
            try
            {
                //TODO: PhuongNL amount error
                var product = new Product()
                {
                    ProductName = request.ProductName,
                    Thumbnail = request.Thumbnail,
                    Description = request.Description,
                    //Amount = request.Amount,
                    Price = request.Price,
                    SalePrice = request.SalePrice,
                    CategoryId = request.CategoryId,
                    Status = Core.Models.Enums.Status.Active,
                    CreatedDateTime = DateTime.Now,
                };
                await _unitOfWork.Products.AddAsync(product);
                int rows = await _unitOfWork.CompleteAsync();

                if (rows > 0)
                {
                    var lastestProduct = _unitOfWork.Products.GetAll().OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
                    var listProductSize = new List<ProductSize>();

                    for (int i = 0; i < request.Sizes.Count; i++)
                    {
                        var productSize = new ProductSize()
                        {
                            ProductId = Convert.ToInt32(lastestProduct?.Id),
                            SizeId = _unitOfWork.Sizes.GetSizeByName(request.Sizes[i]).Id,
                            Quantity = request.Quantities[i],
                            CreatedDateTime = DateTime.Now,
                            Status = Core.Models.Enums.Status.Active
                        };
                        listProductSize.Add(productSize);
                    }
                    if (lastestProduct != null)
                    {
                        lastestProduct.ProductSizes = listProductSize;
                        _unitOfWork.Products.Update(lastestProduct);
                        await _unitOfWork.CompleteAsync();
                    }

                    result.StatusCode = Common.Enums.StatusCode.Success;
                    return result;
                }
                result.StatusCode = Common.Enums.StatusCode.InternalServerError;
                result.ErrorMessage = "Error";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error PostAddProduct" + ex.Message);
            }
            return result;
        }

        public void Post(Comment request)
        {
            var feedBack = new FeedBack
            {
                ProductId = request.ProductID,
                CreatedDateTime = DateTime.Now,
                Comment = request.Message,
                UserId = _userManager.FindByEmailAsync(request.Email).Result.Id,
                Status = Core.Models.Enums.Status.Active,
                RatedStar = Convert.ToDecimal(request.Point)
            };
            _unitOfWork.FeedBacks.Add(feedBack);
            _unitOfWork.Complete();
        }

        public async Task<BaseResultModel> Put(PutUpdateProduct request)
        {
            var result = new BaseResultModel();
            try
            {
                var product = _unitOfWork.Products.GetProductFeedbackById(request.Id);

                if (product != null)
                {
                    if (!string.IsNullOrEmpty(request.ProductName))
                    {
                        product.ProductName = request.ProductName;
                    }
                    if (!string.IsNullOrEmpty(request.Thumbnail))
                    {
                        product.Thumbnail = request.Thumbnail;
                    }

                    //TODO: PhuongNL check amount here
                    //if (request.Amount > 0)
                    //{
                    //    product.Amount = request.Amount;
                    //}
                    if (request.Price > 0)
                    {
                        product.Price = request.Price;
                    }
                    if (request.SalePrice > 0)
                    {
                        product.SalePrice = request.SalePrice;
                    }
                    if (request.CategoryId > 0)
                    {
                        product.CategoryId = request.CategoryId;
                    }
                    if (!string.IsNullOrEmpty(request.Description))
                    {
                        product.Description = request.Description;
                    }

                    product.Status = request.Status;

                    var listProductSize = product.ProductSizes.OrderBy(x => x.Size.SizeType).ToList();

                    for (int i = 0; i < listProductSize.Count; i++)
                    {
                        listProductSize[i].Quantity = request.Quantities[i];
                    }

                    _unitOfWork.Products.Update(product);
                    int rows = await _unitOfWork.CompleteAsync();
                    if (rows > 0)
                    {
                        result.StatusCode = Common.Enums.StatusCode.Success;
                        return result;
                    }
                    result.StatusCode = Common.Enums.StatusCode.InternalServerError;
                    result.ErrorMessage = "Error";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error PutUpdateProduct" + ex.Message);
            }
            return result;
        }
    }
}
