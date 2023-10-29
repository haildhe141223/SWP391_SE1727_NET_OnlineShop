using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
	public class EmailService : BaseService, IEmailService
	{
		private readonly IMapper _mapper;
		private readonly ILoggerService _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		public EmailService(
			IMapper mapper,
			ILoggerService logger,
			IUnitOfWork unitOfWork,
			UserManager<User> userManager)
		{
			_mapper = mapper;
			_logger = logger;
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}
		public async Task<EmailViewModel> Post(EmailModel.PostAddEmail request)
		{

			var result = new EmailViewModel();
			try
			{
				var to = request.To;

				if (!string.IsNullOrEmpty(to))
				{
					var user = await _userManager.FindByEmailAsync(to.Trim());
					if (user == null)
					{
						to = string.Empty;
					}
				}
				if (string.IsNullOrEmpty(to?.Trim()))
				{
					result.StatusCode = Common.Enums.StatusCode.InternalServerError;
					result.ErrorMessage = "User does not exist";
					return result;
				}
				var email = new Email
				{
					To = to,
					Body = request.Body,
					Subject = request.Subject,
					Category = request.Category,
					Status = Core.Models.Enums.Status.Active,
					Title = request.Title
				};
				_unitOfWork.Context.Emails?.Add(email);
				var rows = await _unitOfWork.CompleteAsync();
				if (rows > 0)
				{
					result = _mapper.Map<EmailViewModel>(email);
					result.StatusCode = Common.Enums.StatusCode.Success;
					return result;
				}
				result.StatusCode = Common.Enums.StatusCode.InternalServerError;
			}
			catch (Exception ex)
			{
				_logger.LogError("Error at PostAddEmail " + ex);
			}
			return result;
		}
	}
}
