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
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class SizeService : BaseService, ISizeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;

        public SizeService(
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

        public async Task<BaseResultModel> Delete(DeleteSize request)
        {
            var result = new BaseResultModel();
            try
            {
                _unitOfWork.Sizes.Delete(request.SizeId, request.IsHardDelete);
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

        public List<SizeViewModel> Get(GetAllSize request)
        {
            var result = new List<SizeViewModel>();
            try
            {
                var sizes = _unitOfWork.Sizes.GetAll()
                    .OrderByDescending(x => x.CreatedDateTime).ToList();
                result = _mapper.Map<List<SizeViewModel>>(sizes);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<SizeViewModel> Get(GetAllActiveSize request)
        {
            var result = new List<SizeViewModel>();
            try
            {
                var sizes = _unitOfWork.Sizes.GetAll().Where(x => x.Status == Core.Models.Enums.Status.Active)
                    .OrderBy(x => x.SizeType).ToList();
                result = _mapper.Map<List<SizeViewModel>>(sizes);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public SizeViewModel Get(GetSizeById request)
        {
            var result = new SizeViewModel();
            try
            {
                var size = _unitOfWork.Sizes.GetById(request.SizeId);
                if (size != null)
                {
                    result = _mapper.Map<SizeViewModel>(size);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<BaseResultModel> Post(PostAddSize request)
        {
            var result = new BaseResultModel();
            try
            {
                var size = new Size()
                {
                    SizeType = request.SizeType,
                    Status = Core.Models.Enums.Status.Active,
                    CreatedDateTime = DateTime.Now
                };
                await _unitOfWork.Sizes.AddAsync(size);
                int rows = await _unitOfWork.CompleteAsync();
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
                _logger.LogError("Error PostAddSize" + ex.Message);
            }
            return result;
        }

        public async Task<BaseResultModel> Put(PutUpdateSize request)
        {
            var result = new BaseResultModel();
            try
            {
                var size = await _unitOfWork.Sizes.GetByIdAsync(request.Id);

                if (size != null)
                {
                    if (!string.IsNullOrEmpty(request.SizeType))
                    {
                        size.SizeType = request.SizeType;
                    }
                    size.Status = request.Status;

                    _unitOfWork.Sizes.Update(size);
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
                _logger.LogError("Error PutUpdateSize" + ex.Message);
            }
            return result;
        }
    }
}
