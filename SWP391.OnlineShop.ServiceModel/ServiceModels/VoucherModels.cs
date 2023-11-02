using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
	public class VoucherModels
	{
		[Route("/Voucher/GetAllVoucherByUser", "GET")]
		public class GetAllVoucherByUser : IReturn<List<VoucherViewModels>>
		{
			public string Email { get; set; }
		}

		[Route("/Voucher/GetVoucherById", "GET")]
		public class GetVoucherById : IReturn<VoucherViewModels>
		{
			public int Id { get; set; }
		}

		[Route("/Voucher/PostAddVoucher", "POST")]
		public class PostAddVoucher : IReturn<BaseResultModel>
		{
			public string Email { get; set; }
			public string VoucherName { get; set; }
			public string VoucherCode { get; set; }
			public int Amount { get; set; }
			public DateTime StartDateTime { get; set; }
			public DateTime EndDateTime { get; set; }
			public VoucherType Type { get; set; }
			public decimal Value { get; set; }
            public List<int> ProductId { get; set; }
        }

		[Route("/Voucher/PutUpdateVoucher", "PUT")]
		public class PutUpdateVoucher : IReturn<BaseResultModel>
		{
			public int Id { get; set; }
			public string Email { get; set; }
			public string VoucherName { get; set; }
			public string VoucherCode { get; set; }
			public int Amount { get; set; }
			public DateTime StartDateTime { get; set; }
			public DateTime EndDateTime { get; set; }
            public VoucherType Type { get; set; }
            public decimal Value { get; set; }
        }

		[Route("/Voucher/DeleteVoucher", "DELETE")]
		public class DeleteVoucher : IReturn<BaseResultModel>
		{
			public int Id { get; set; }
		}

		[Route("/Voucher/GetAllAvailableVoucher", "GET")]
		public class GetAllAvailableVoucher : IReturn<List<VoucherViewModels>>
		{
		}

		[Route("/Voucher/PostAddVoucherToUser", "POST")]
		public class PostAddVoucherToUser : IReturn<BaseResultModel>
		{
			public int VoucherId { get; set; }
			public int UserId { get; set; }
		}

		[Route("/Voucher/PutUpdateVoucherAmount", "PUT")]
		public class PutUpdateVoucherAmount : IReturn<BaseResultModel>
		{
			public int Id { get; set; }
		}
	}
}
