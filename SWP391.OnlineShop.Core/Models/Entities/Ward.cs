using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities
{
	public class Ward : BaseEntity<int>
	{
        public int DistrictId { get; set; }
        public District District { get; set; }
        public string WardName { get; set; }
	}
}
