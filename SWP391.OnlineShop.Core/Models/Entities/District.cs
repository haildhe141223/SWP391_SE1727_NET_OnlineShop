using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities
{
	public class District : BaseEntity<int>
	{
        public District()
        {
            Wards = new HashSet<Ward>();
        }
        public Province Province { get; set; }
        public int ProvinceId { get; set; }
        public string DistrictName { get; set; }
        public ICollection<Ward> Wards { get; set; }
	}
}
