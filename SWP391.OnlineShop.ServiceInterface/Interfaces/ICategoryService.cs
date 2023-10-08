using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Category;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceInterface.Interfaces
{
	public interface ICategoryService
	{
		/// <summary>
		/// Get all categories
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Get all categories</returns>
		List<CategoryViewModel> Get(GetAllCategory request);
	}
}
