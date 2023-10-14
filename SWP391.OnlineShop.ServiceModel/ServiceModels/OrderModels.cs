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
        public class GetCartDetailByUser : IReturn<OrderViewModel>
        {
            public string Email { get; set; }
        }

        [Route("/Order/GetCartContactByUser", "GET")]
        public class GetCartContactByUser : IReturn<OrderViewModel>
        {
            public string Email { get; set; }
        }

        [Route("/Order/GetCartCompletionByUser", "GET")]
        public class GetCartCompletionByUser : IReturn<OrderViewModel>
        {
            public string Email { get; set; }
        }

        [Route("/Order/GetCartInfo", "GET")]
        public class GetCartInfo : IReturn<OrderViewModel>
        {
            public int Id { get; set; }
        }

        [Route("/Order/PostAddToCart", "POST")]
        public class PostAddToCart : IReturn<OrderViewModel>
        {
            public string Email { get; set; }
            public List<OrderDetail> OrderDetails { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public Gender CustomerGender { get; set; }
            public string CustomerEmail { get; set; }
            public decimal TotalCost { get; set; }
            public OrderStatus OrderStatus { get; set; }
        }

        [Route("/Order/PutUpdateCart", "PUT")]
        public class PutUpdateCart : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
            public List<OrderDetail> OrderDetails { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public Gender CustomerGender { get; set; }
            public string CustomerEmail { get; set; }
            public decimal TotalCost { get; set; }
            public OrderStatus OrderStatus { get; set; }
        }

		[Route("/Order/PutUpdateCartStatus", "PUT")]
		public class PutUpdateCartStatus : IReturn<BaseResultModel>
		{
			public int Id { get; set; }
			public OrderStatus OrderStatus { get; set; }
		}

		[Route("/Order/PutUpdateCartToContact", "PUT")]
        public class PutUpdateCartToContact : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
            public decimal TotalCost { get; set; }
            public OrderStatus OrderStatus { get; set; }
            public string Address { get; set; }
        }

        [Route("/Order/PutUpdateQuantity", "PUT")]
        public class PutUpdateQuantity : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
            public int Quantity { get; set; }

        }

        [Route("/Order/DeleteCart", "DELETE")]
        public class DeleteCart : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
        }

        [Route("/Order/DeleteOrderDetail", "DELETE")]
        public class DeleteOrderDetail : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
        }
    }
}
