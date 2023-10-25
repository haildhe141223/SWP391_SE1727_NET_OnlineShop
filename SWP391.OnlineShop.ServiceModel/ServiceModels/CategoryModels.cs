using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    [Route("/Category/GetAllCategory", "GET")]
    public class GetAllCategory : IReturn<List<CategoryViewModel>>
    {
    }
}
