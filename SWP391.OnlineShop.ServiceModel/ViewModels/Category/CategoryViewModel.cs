using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
        public List<Product> Products { get; set; }

    }
}
