using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
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
        public List<CategoryViewModel> Get(GetAllCategory request)
        {
            var result = new List<CategoryViewModel>();
            try
            {
                var categories = _unitOfWork.Categories.GetAll().Where(x => x.CategoryType == request.CategoryType);
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
    }
}
