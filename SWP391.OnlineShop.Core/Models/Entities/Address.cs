using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.Entities
{
	public class Address : BaseEntity<int>
	{
        public string FullAddress { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; }
    }
}
