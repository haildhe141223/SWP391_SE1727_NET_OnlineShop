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
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
    public class FeedbackService : BaseService, IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;

        public FeedbackService(
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
        public List<FeedbackViewModels> Get(FeedbackModels.GetAllFeedback request)
        {
            var result = new List<FeedbackViewModels>();
            try
            {
                var feedbacks = _unitOfWork.FeedBacks.GetAll().ToList();
                foreach (var feedback in feedbacks)
                {
                    var feedbackVm = _mapper.Map<FeedbackViewModels>(feedback);
                    feedbackVm.ProductName = _unitOfWork.Products.GetById(feedback.ProductId).ProductName;
                    feedbackVm.UserName = _userManager.FindByIdAsync(Convert.ToString(feedback.UserId)).Result.UserName;
                    result.Add(feedbackVm);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public FeedbackViewModels Get(FeedbackModels.GetFeedbackById request)
        {
            var result = new FeedbackViewModels();
            try
            {
                var feedback = _unitOfWork.FeedBacks.GetById(request.FeedbackId);
                var user = _userManager.FindByIdAsync(Convert.ToString(feedback.UserId)).Result;
                result = _mapper.Map<FeedbackViewModels>(feedback);
                result.ProductName = _unitOfWork.Products.GetById(feedback.ProductId).ProductName;
                result.UserName = user.UserName;
                result.Email = user.Email;
                result.Mobile = user.PhoneNumber;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<BaseResultModel> Put(FeedbackModels.EditFeedback request)
        {
            var result = new BaseResultModel();

            var feedback = _unitOfWork.FeedBacks.GetById(request.FeedbackId);
            if(feedback != null)
            {
                feedback.Status = request.Status;
                _unitOfWork.FeedBacks.Update(feedback);
                int rows = await _unitOfWork.CompleteAsync();
                if (rows > 0)
                {
                    result.StatusCode = Common.Enums.StatusCode.Success;
                    return result;
                }
                result.StatusCode = Common.Enums.StatusCode.InternalServerError;
                result.ErrorMessage = "Error";
            }
            return result;
        }
    }
}
