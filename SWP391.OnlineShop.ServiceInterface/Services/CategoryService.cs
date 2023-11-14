using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    //TODO: PhuongNL logger should have key to double check in log. Check AccountService for example
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public CategoryService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILoggerService logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResultModel> Delete(DeleteCategory request)
        {
            try
            {
                var category = _unitOfWork.Categories.GetById(request.Id);
                if(category  == null)
                {
                    return new BaseResultModel()
                    {
                        StatusCode = Common.Enums.StatusCode.NotFound,
                        ErrorMessage = "Category Not Found"
                    };
                }
                _unitOfWork.Categories.Delete(category);
                int rows = await _unitOfWork.CompleteAsync();
                if(rows > 0)
                {
                    return new BaseResultModel()
                    {
                        StatusCode = Common.Enums.StatusCode.Success
                    };
                }
                return new BaseResultModel()
                {
                    StatusCode = Common.Enums.StatusCode.InternalServerError,
                    ErrorMessage = "Bad Request"
                }; ;
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteCategory err " + ex.Message);
            }
            return new BaseResultModel();
        }

        public List<CategoryViewModel> Get(GetAllCategory request)
        {
            var result = new List<CategoryViewModel>();
            try
            {
                var categories = _unitOfWork.Categories.GetAll().Where(x => x.CategoryType == request.CategoryType 
                                            && x.Status == Core.Models.Enums.Status.Active);
                foreach (var item in categories)
                {
                    var categoryVm = _mapper.Map<CategoryViewModel>(item);
                    categoryVm.TotalProduct = item.Products.Count();
                    categoryVm.Thumbnail = item.Products.FirstOrDefault()?.Thumbnail;
                    result.Add(categoryVm);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<CategoryViewModel> Get(GetAllCategories request)
        {
            var result = new List<CategoryViewModel>();
            try
            {
                var categories = _unitOfWork.Categories.GetAll();
                foreach (var item in categories)
                {
                    var categoryVm = _mapper.Map<CategoryViewModel>(item);
                    result.Add(categoryVm);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllCategories err "+ ex.Message);
            }
            return result;
        }

        public CategoryViewModel Get(GetCategoryById request)
        {
            try
            {
                var category = _unitOfWork.Categories.GetById(request.Id);
                if (category == null)
                {
                    var result = new CategoryViewModel()
                    {
                        StatusCode = Common.Enums.StatusCode.NotFound,
                        ErrorMessage = "Category Not Found"
                    };
                    return result;
                }
                return _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteCategory err " + ex.Message);
            }
            return new CategoryViewModel();
        }

        public async Task<BaseResultModel> Post(PostAddCategory request)
        {
            var result = new BaseResultModel();
            try
            {
                var category = new Category()
                {
                    CategoryName = request.CategoryName,
                    CategoryType = request.CategoryType,
                };
                _unitOfWork.Categories.Add(category);
                int rows = await _unitOfWork.CompleteAsync();
                if(rows > 0)
                {
                    result.StatusCode = Common.Enums.StatusCode.Success;
                    return result;
                }
                result.StatusCode = Common.Enums.StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostAddCategory error {ex.Message}");
            }
            return result;
        }

        public async Task<BaseResultModel> Put(PutUpdateCategory request)
        {
            var result = new BaseResultModel();
            try
            {
                var category = _unitOfWork.Categories.GetById(request.Id);
                if (category == null)
                {
                    result.StatusCode = Common.Enums.StatusCode.NotFound;
                    result.ErrorMessage = "Category Not Found";
                    return result;
                }
                category.CategoryName = request.CategoryName;
                category.CategoryType = request.CategoryType;
                _unitOfWork.Categories.Update(category);
                int rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    result.StatusCode = Common.Enums.StatusCode.Success;
                    return result;
                }
                result.StatusCode = Common.Enums.StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutUpdateCategory error {ex.Message}");
            }
            return result;
        }
    }
}
