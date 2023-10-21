using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Customer;
using SWP391.OnlineShop.ServiceModel.ViewModels.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<SliderViewModel> Delete(SliderModel.DeleteSlider request)
        {
            var result = new SliderViewModel();
            try
            {
                _unitOfWork.Sliders.Delete(request.SliderId);
                var rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    var slider = await _unitOfWork.Sliders.GetByIdAsync(request.SliderId);
                    result = _mapper.Map<SliderViewModel>(slider);
                    //result.StatusCode = StatusCode.Success;
                    return result;
                }
                //result.StatusCode = StatusCode.InternalServerError;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<SliderViewModel> Get(SliderModel.GetAllSlider request)
        {
            var result = new List<SliderViewModel>();
            try
            {
                var slider = _unitOfWork.Sliders.GetAll().ToList();
                result = _mapper.Map<List<SliderViewModel>>(slider);
                //result.StatusCode = StatusCode.Success;
                return result;
                //result.StatusCode = StatusCode.InternalServerError;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
                    //result.StatusCode = StatusCode.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<SliderViewModel> Post(SliderModel.PostAddSlider request)
        {
            var result = new SliderViewModel();
            try
            {
                var slider = new Slider()
                {
                    Title = request.Title,
                    Image = result.Image,
                    BlackLink = request.BlackLink,
                    SliderStatus = Core.Models.Enums.Status.Active
                };
                await _unitOfWork.Sliders.AddAsync(slider);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<SliderViewModel> Put(SliderModel.PutUpdateSlider request)
        {
            var result = new SliderViewModel();
            try
            {
                var slider = _unitOfWork.Sliders.GetById(request.Id);
                if (slider == null)
                {
                    //result.StatusCode = StatusCode.InternalServerError;
                    //return result;
                }
                slider.Title = request.Title;

                _unitOfWork.Sliders.Update(slider);
                var rows = await _unitOfWork.CompleteAsync();
                //result.StatusCode = rows > 0 ? StatusCode.Success : StatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }
    }
}
