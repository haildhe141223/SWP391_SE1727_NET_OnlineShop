using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.BaseServices;
using SWP391.OnlineShop.ServiceInterface.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Category;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceInterface.Services
{
	public class CategoryService : BaseService, ICategoryService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILoggerService _logger;
		private readonly UserManager<User> _userManager;

		public CategoryService(
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
		public List<CategoryViewModel> Get(GetAllCategory request)
		{
			var result = new List<CategoryViewModel>();
			try
			{
				var categories = _unitOfWork.Categories.GetAll();
				foreach (var item in categories)
				{
					var categoryVm = _mapper.Map<CategoryViewModel>(item);
					categoryVm.TotalProduct = item.Products.Count();
					result.Add(categoryVm);
				}
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
	}
}
