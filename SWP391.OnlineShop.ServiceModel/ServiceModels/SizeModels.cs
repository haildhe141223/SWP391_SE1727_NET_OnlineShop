using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    [Route("/Size/GetAllSize", "GET")]
    public class GetAllSize : IReturn<List<SizeViewModel>>
    {
    }

    [Route("/Size/GetAllActiveSize", "GET")]
    public class GetAllActiveSize : IReturn<List<SizeViewModel>>
    {
    }

    [Route("/Size/GetSizeById", "GET")]
    public class GetSizeById : IReturn<SizeViewModel>
    {
        public int SizeId { get; set; }
    }

    [Route("/Size/PostAddSize", "POST")]
    public class PostAddSize : IReturn<BaseResultModel>
    {
        public string SizeType { get; set; }
    }

    [Route("/Size/PutUpdateSize", "PUT")]
    public class PutUpdateSize : IReturn<BaseResultModel>
    {
        public string SizeType { get; set; }
        public int Id { get; set; }
        public Status Status { get; set; }
    }

    [Route("/Size/DeleteSize", "DELETE")]
    public class DeleteSize : IReturn<BaseResultModel>
    {
        public int SizeId { get; set; }
        public bool IsHardDelete { get; set; }
    }
}
