using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class ProductCategoryViewModel
    {
        public List<ProductViewModel> LatestProducts { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductViewModel> ProductsOfWeek { get; set; }
        public List<TagViewModel> Tags { get; set; }


    }
}
