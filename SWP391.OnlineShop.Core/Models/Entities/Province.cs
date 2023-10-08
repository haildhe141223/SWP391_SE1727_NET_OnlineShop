using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities
{
	public class Province : BaseEntity<int>
	{
        public Province()
        {
            Districts = new HashSet<District>();
        }
        public string ProvinceName { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
