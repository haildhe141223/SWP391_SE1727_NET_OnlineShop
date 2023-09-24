using AutoMapper;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Cart;

namespace SWP391.OnlineShop.Service.Configs.AutoMapper;

public class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        //// Project
        CreateMap<Order, OrderDetailViewModel>().ReverseMap();
    }
}