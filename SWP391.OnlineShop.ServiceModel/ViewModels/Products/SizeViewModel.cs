using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
	public class SizeViewModel
	{
		public int Id { get; set; }
		public string SizeType { get; set; }
        public Status Status { get; set; }
    }
}
