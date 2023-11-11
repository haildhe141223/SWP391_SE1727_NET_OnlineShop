using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.Entities
{
    public class Address : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string PhoneNumber { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; }
    }
}
