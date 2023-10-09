using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.OnlineShop.Core.Models.Entities
{
    public class Slider : BaseEntity<int>
    {
        public Slider() { }

        public string Title { get; set; }
        public string Image { get; set; }
        public string BlackLink { get; set; }
        public Status SliderStatus { get; set; }
    }
}
