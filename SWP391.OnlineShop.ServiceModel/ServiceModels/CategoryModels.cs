using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    [Route("/Category/GetAllCategory", "GET")]
    public class GetAllCategory : IReturn<List<CategoryViewModel>>
    {
        public CategoryType CategoryType { get; set; }
    }

    [Route("/Category/GetCategoryById", "GET")]
    public class GetCategoryById : IReturn<CategoryViewModel>
    {
        public int Id { get; set; }
    }

    [Route("/Category/GetAllCategories", "GET")]
    public class GetAllCategories : IReturn<List<CategoryViewModel>>
    {
    }

    [Route("/Category/PostAddCategory", "POST")]
    public class PostAddCategory : IReturn<BaseResultModel>
    {
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
    }

    [Route("/Category/PutUpdateCategory", "PUT")]
    public class PutUpdateCategory : IReturn<BaseResultModel>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
    }

    [Route("/Category/DeleteCategory", "DELETE")]
    public class DeleteCategory : IReturn<BaseResultModel>
    {
        public int Id { get; set; }
    }
}
