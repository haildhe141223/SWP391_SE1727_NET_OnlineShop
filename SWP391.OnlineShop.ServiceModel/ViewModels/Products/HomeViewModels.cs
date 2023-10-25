using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Products
{
    public class HomeViewModels
    {
        public List<ProductViewModel> LatestProducts { get; set; }
        public List<ProductViewModel> HotDealProduct { get; set; }
        public ProductViewModel ProductDetail { get; set; }
        public List<ProductViewModel> ProductsOfWeek { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
