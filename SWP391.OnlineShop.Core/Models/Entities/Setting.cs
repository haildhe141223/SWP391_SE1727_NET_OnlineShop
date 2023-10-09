using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.Core.Models.Entities
{
    public class Setting: BaseEntity<int>
    {
        public Setting() { }

        public string Type { get; set; }
        public decimal Value { get; set; }
        public int? OrderId { get; set; }
        public Status SettingStatus { get; set; }
        public Order Order { get; set; }
    }
}
