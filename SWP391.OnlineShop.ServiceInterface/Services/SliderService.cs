using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class SliderService : BaseService, ISliderService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public SliderService(IMapper mapper, IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResultModel> Delete(SliderModel.DeleteSlider request)
        {
            var result = new BaseResultModel();
            try
            {
                _unitOfWork.Sliders.Delete(request.SliderId, request.IsHardDelete);
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
                //TODO: SangDN logger should have key to double check in log. Check AccountService for example
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<SliderViewModel> Get(SliderModel.GetAllSlider request)
        {
            var result = new List<SliderViewModel>();
            try
            {
                var sliders = _unitOfWork.Sliders.GetAll().ToList();
                foreach (var slider in sliders)
                {
                    var sliderVm = _mapper.Map<SliderViewModel>(slider);
                    result.Add(sliderVm);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Slider Service - GetAllSlider()");
            }
            return result;
        }

        public SliderViewModel Get(SliderModel.GetSliderById request)
        {
            var result = new SliderViewModel();
            try
            {
                var appendix = _unitOfWork.Sliders.GetById(request.SliderId);
                if (appendix != null)
                {
                    result = _mapper.Map<SliderViewModel>(appendix);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Slider Service - GetSliderById()");
            }
            return result;
        }

        public async Task<BaseResultModel> Post(SliderModel.PostAddSlider request)
        {
            var result = new BaseResultModel();
            try
            {
                var slider = new Slider()
                {
                    Title = request.Title,
                    Image = request.Image,
                    BlackLink = request.BlackLink,
                    SliderStatus = Core.Models.Enums.Status.Active
                };
                await _unitOfWork.Sliders.AddAsync(slider);
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
                //TODO: SangDN logger should have key to double check in log. Check AccountService for example
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<BaseResultModel> Put(SliderModel.PutUpdateSlider request)
        {
            var result = new BaseResultModel();
            try
            {
                var slider = await _unitOfWork.Sliders.GetByIdAsync(request.Id);

                if (slider != null)
                {
                    slider.Title = request.Title;
                    slider.Image = request.Image;
                    slider.BlackLink = request.BlackLink;
                    slider.Status = request.Status;

                    _unitOfWork.Sliders.Update(slider);
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
                //TODO: SangDN logger should have key to double check in log. Check AccountService for example
                _logger.LogError(ex.Message);
            }
            return result;
        }
    }
}
