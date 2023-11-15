using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    [Route("/Tag/GetAllProductTag", "GET")]
    public class GetAllProductTag : IReturn<List<TagViewModel>>
    {
    }
}
