using Microsoft.AspNetCore.Http;
using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{

    [Route("/Product/GetAllProduct", "GET")]
    public class GetAllProduct : IReturn<List<ProductViewModel>>
    {
    }

    [Route("/Product/GetAllActiveProduct", "GET")]
    public class GetAllActiveProduct : IReturn<List<ProductViewModel>>
    {
    }

    [Route("/Product/GetProductByCategoryId", "GET")]
    public class GetProductByCategoryId : IReturn<List<ProductViewModel>>
    {
        public int? CategoryId { get; set; }
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
    public class PostAddProduct : IReturn<BaseResultModel>
    {
        public string ProductName { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int? CategoryId { get; set; }
        public List<string> Sizes { get; set; }
        public List<int> Quantities { get; set; }
    }

    [Route("/Product/PutUpdateProduct", "PUT")]
    public class PutUpdateProduct : IReturn<BaseResultModel>
    {
        public string ProductName { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int? CategoryId { get; set; }
        public int Id { get; set; }
        public Status Status { get; set; }
        public List<string> Sizes { get; set; }
        public List<int> Quantities { get; set; }
    }

    [Route("/Product/DeleteProduct", "DELETE")]
    public class DeleteProduct : IReturn<BaseResultModel>
    {
        public int ProductId { get; set; }
        public bool IsHardDelete { get; set; }
    }

    [Route("/Product/Comment", "POST")]
    public class Comment : IReturnVoid
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Point { get; set; }
        public string Message { get; set; }
        public int ProductID { get; set; }
    }

	[Route("/Product/GetProductOfVoucher", "GET")]
	public class GetProductOfVoucher : IReturn<List<ProductViewModel>>
	{
        public int VoucherId { get; set; }
    }
}
