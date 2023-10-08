using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{

    [Route("/Product/GetAllProduct", "GET")]
    public class GetAllProduct : IReturn<List<ProductViewModel>>
    {
    }

    [Route("/Product/GetHotDealProduct", "GET")]
    public class GetHotDealProduct : IReturn<List<ProductViewModel>>
    {
    }

    [Route("/Product/GetDealProductOfWeek", "GET")]
    public class GetDealProductOfWeek : IReturn<List<ProductViewModel>>
    {
    }

    [Route("/Product/GetProductById", "GET")]
    public class GetProductById : IReturn<ProductViewModel>
    {
        public int ProductId { get; set; }
    }

    [Route("/Product/GetProductFeedbackById", "GET")]
    public class GetProductFeedbackById : IReturn<ProductViewModel>
    {
        public int ProductId { get; set; }
    }

    [Route("/Product/PostAddProduct", "POST")]
    public class PostAddProduct : IReturn<ProductViewModel>
    {
        public string ProductName { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int? CategoryId { get; set; }
    }

    [Route("/Product/PutUpdateProduct", "PUT")]
    public class PutUpdateProduct : IReturn<ProductViewModel>
    {

        public string ProductName { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int? CategoryId { get; set; }
        public int Id { get; set; }
    }

    [Route("/Product/DeleteProduct", "DELETE")]
    public class DeleteProduct : IReturn<ProductViewModel>
    {
        public int ProductId { get; set; }
    }
}
