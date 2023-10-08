using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Category;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
	[Route("/Category/GetAllCategory", "GET")]
	public class GetAllCategory : IReturn<List<CategoryViewModel>>
	{
	}
}
