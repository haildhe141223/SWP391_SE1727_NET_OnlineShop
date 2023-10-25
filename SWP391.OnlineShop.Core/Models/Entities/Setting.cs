using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Models.Entities
{
    public class Setting : BaseEntity<int>
    {
        public string Type { get; set; }
        public decimal Value { get; set; }
        public Status SettingStatus { get; set; }
    }
}
