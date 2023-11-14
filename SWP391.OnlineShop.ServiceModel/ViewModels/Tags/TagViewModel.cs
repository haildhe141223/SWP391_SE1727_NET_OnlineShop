using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Tags
{
	public class TagViewModel
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string TagName { get; set; }
        public Status Status { get; set; }
    }
}
