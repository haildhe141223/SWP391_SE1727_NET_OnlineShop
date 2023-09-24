using AutoMapper;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;

namespace SWP391.OnlineShop.Service.Configs.AutoMapper;

public class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        //// Product
        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<Post, PostViewModel>().ReverseMap();
    }
}