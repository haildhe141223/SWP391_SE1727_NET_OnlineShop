using AutoMapper;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.Service.Configs.AutoMapper;

public class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        //// Order
        CreateMap<Order, OrderViewModels>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailViewModels>().ReverseMap();
        //// Product
        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<Post, PostViewModel>().ReverseMap();
        //// Category
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        //// Feedback
        CreateMap<FeedBack, FeedbackViewModels>().ReverseMap();

        ////Email
        CreateMap<Email, EmailViewModel>().ReverseMap();
    }
}