using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;

namespace SWP391.OnlineShop.Service.Configs.AutoMapper;

public class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs(IUnitOfWork? unitOfWork)
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

        //// Email
        CreateMap<Email, EmailViewModel>().ReverseMap();

		//// Voucher
		CreateMap<Voucher, VoucherViewModels>().ReverseMap();
		CreateMap<ProductVoucherViewModel, ProductVoucher>().ReverseMap();

		if (unitOfWork != null)
        {
            //// User
            CreateMap<User, UserViewModel>()
                .ForMember(des => des.Role, mem => mem.MapFrom(src => unitOfWork.Settings.GetRolesByUserId(src.Id)))
                .ForMember(des => des.Avatar, mem => mem.MapFrom(src => src.Image));
        }
    }
}