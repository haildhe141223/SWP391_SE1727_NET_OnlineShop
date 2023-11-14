using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.SettingModels;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class SettingService : BaseService, ISettingService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public SettingService(IMapper mapper, IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SettingViewModel> Delete(DeleteSetting request)
        {
            var result = new SettingViewModel();
            try
            {
                _unitOfWork.Settings.Delete(request.SettingId);
                var rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    var setting = await _unitOfWork.Settings.GetByIdAsync(request.SettingId);
                    result = _mapper.Map<SettingViewModel>(setting);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TODO: SangDN logger should have key to double check in log. Check AccountService for example
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public List<SettingViewModel> Get(GetAllSetting request)
        {
            var result = new List<SettingViewModel>();
            try
            {
                var setting = _unitOfWork.Settings.GetAll().ToList();
                result = _mapper.Map<List<SettingViewModel>>(setting);
                return result;
            }
            catch (Exception ex)
            {
                //TODO: SangDN logger should have key to double check in log. Check AccountService for example
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public SettingViewModel Get(GetSettingById request)
        {
            var result = new SettingViewModel();
            try
            {
                var appendix = _unitOfWork.Settings.GetById(request.SettingId);
                if (appendix != null)
                {
                    result = _mapper.Map<SettingViewModel>(appendix);
                }
            }
            catch (Exception ex)
            {
                //TODO: SangDN logger should have key to double check in log. Check AccountService for example
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<SettingViewModel> Post(PostAddSetting request)
        {
            var result = new SettingViewModel();
            try
            {
                var setting = new Setting
                {
                    Type = request.Type,
                };
                await _unitOfWork.Settings.AddAsync(setting);

            }
            catch (Exception ex)
            {
                //TODO: SangDN logger should have key to double check in log. Check AccountService for example
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<SettingViewModel> Put(PutUpdateSetting request)
        {
            var result = new SettingViewModel();
            try
            {
                var setting = await _unitOfWork.Settings.GetByIdAsync(request.Id);

                if (setting != null)
                {
                    setting.Type = request.Type;

                    _unitOfWork.Settings.Update(setting);
                    await _unitOfWork.CompleteAsync();
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
