using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities
{
    public class Size : BaseEntity<int>
    {
        public Size()
        {
            ProductSizes = new HashSet<ProductSize>();
        }
        public string SizeType { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
