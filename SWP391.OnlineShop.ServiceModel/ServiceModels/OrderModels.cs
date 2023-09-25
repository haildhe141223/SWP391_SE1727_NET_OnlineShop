using ServiceStack;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Cart;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    public class OrderModels
    {
        [Route("/Order/GetCartDetailByUser", "GET")]
        public class GetCartDetailByUser : IReturn<List<OrderDetailViewModel>>
        {
            public string? Email { get; set; }
        }

        [Route("/Order/GetCartContactByUser", "GET")]
        public class GetCartContactByUser : IReturn<List<OrderDetailViewModel>>
        {
            public string? Email { get; set; }
        }

        [Route("/Order/GetCartCompletionByUser", "GET")]
        public class GetCartCompletionByUser : IReturn<List<OrderDetailViewModel>>
        {
            public string? Email { get; set; }
        }

        [Route("/Order/PostAddToCart", "Post")]
        public class PostAddToCart : IReturn<List<OrderDetailViewModel>>
        {
            public List<OrderDetail>? OrderDetails { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public Gender CustomerGender { get; set; }
            public string CustomerEmail { get; set; }
            public decimal TotalCost { get; set; }
        }

        [Route("/Order/PutUpdateCart", "Put")]
        public class PutUpdateCart : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
            public List<OrderDetail>? OrderDetails { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public Gender CustomerGender { get; set; }
            public string CustomerEmail { get; set; }
            public decimal TotalCost { get; set; }
        }

        [Route("/Order/DeleteCart", "Delete")]
        public class DeleteCart : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
        }
    }
}
