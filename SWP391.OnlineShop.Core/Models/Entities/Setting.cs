using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities
{
    public class Setting : BaseEntity<int>
    {
        public string Type { get; set; }
    }
}
