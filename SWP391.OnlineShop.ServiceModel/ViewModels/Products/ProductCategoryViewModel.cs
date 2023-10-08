using SWP391.OnlineShop.ServiceModel.ViewModels.Category;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class ProductCategoryViewModel
    {
        public List<ProductViewModel> LatestProducts { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductViewModel> ProductsOfWeek { get; set; }

    }
}
