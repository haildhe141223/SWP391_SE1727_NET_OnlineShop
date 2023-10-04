using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Customer;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<SettingViewModel> Delete(SettingModels.DeleteSetting request)
        {
            var result = new SettingViewModel();
            try
            {
                _unitOfWork.Settings.Delete(request.ProductId);
                var rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
                    result = _mapper.Map<ProductViewModel>(product);
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

        public List<SettingViewModel> Get(SettingModels.GetAllSetting request)
        {
            throw new NotImplementedException();
        }

        public SettingViewModel Get(SettingModels.GetSettingById request)
        {
            throw new NotImplementedException();
        }

        public Task<SettingViewModel> Post(SettingModels.PostAddSetting request)
        {
            throw new NotImplementedException();
        }

        public Task<SettingViewModel> Put(SettingModels.PutUpdateSetting request)
        {
            throw new NotImplementedException();
        }
    }
}
