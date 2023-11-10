using ServiceStack;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    public class OrderModels
    {
        [Route("/Order/GetAllOrderByUser", "GET")]
        public class GetAllOrderByUser : IReturn<List<OrderViewModels>>
        {
            public string Email { get; set; }
        }
        [Route("/Order/GetCartDetailByUser", "GET")]
        public class GetCartDetailByUser : IReturn<OrderViewModels>
        {
            public string Email { get; set; }
        }

        [Route("/Order/GetCartContactByUser", "GET")]
        public class GetCartContactByUser : IReturn<OrderViewModels>
        {
            public string Email { get; set; }
        }

        [Route("/Order/GetCartCompletionByUser", "GET")]
        public class GetCartCompletionByUser : IReturn<OrderViewModels>
        {
            public string Email { get; set; }
        }

        [Route("/Order/GetCartInfo", "GET")]
        public class GetCartInfo : IReturn<OrderViewModels>
        {
            public int Id { get; set; }
        }

        [Route("/Order/PostAddToCart", "POST")]
        public class PostAddToCart : IReturn<OrderViewModels>
        {
            public int ProductId { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
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
            public string OrderNotes { get; set; }
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

        [Route("/Order/PutUpdateOrderNotes", "PUT")]
        public class PutUpdateOrderNotes : IReturn<BaseResultModel>
        {
            public int Id { get; set; }
            public string OrderNotes { get; set; }
        }

		[Route("/Order/GetAllOrder", "GET")]
		public class GetAllOrder : IReturn<List<OrderViewModels>>
		{
		}
	}
}
