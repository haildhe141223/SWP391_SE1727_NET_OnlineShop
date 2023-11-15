using AutoMapper;
using SWP391.OnlineShop.Core.Cores.UnitOfWork;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
using SWP391.OnlineShop.ServiceModel.ViewModels.Categories;
using SWP391.OnlineShop.ServiceModel.ViewModels.Emails;
using SWP391.OnlineShop.ServiceModel.ViewModels.Feedback;
using SWP391.OnlineShop.ServiceModel.ViewModels.Products;
using SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;
using SWP391.OnlineShop.ServiceModel.ViewModels.Requests;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;
using SWP391.OnlineShop.ServiceModel.ViewModels.Tags;
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
        CreateMap<ProductSize, ProductSizeViewModel>().ReverseMap();
        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<Post, PostViewModel>().ReverseMap();
        CreateMap<Post, ManagePostViewModel>().ReverseMap();
        CreateMap<Tag, TagViewModel>().ReverseMap();
        CreateMap<Size, SizeViewModel>().ReverseMap();

        //// Category
        CreateMap<Category, CategoryViewModel>().ReverseMap();

        //// Feedback
        CreateMap<FeedBack, FeedbackViewModels>().ReverseMap();

        //// Email
        CreateMap<Email, EmailViewModel>().ReverseMap();

        //// Voucher
        CreateMap<Voucher, VoucherViewModels>().ReverseMap();
        CreateMap<OrderVoucherViewModel, OrderVoucher>().ReverseMap();
        CreateMap<Voucher, UserVoucherViewModel>().ReverseMap();

        //// Slider
        CreateMap<Slider, SliderViewModel>().ReverseMap();

        //// Address
        CreateMap<Address, AddressViewModel>().ReverseMap();

        //// Contact
        CreateMap<Email, EmailContactViewModel>().ReverseMap();

        //// Request
        CreateMap<Request, RequestDataViewModel>().ReverseMap();

        if (unitOfWork != null)
        {
            //// User
            CreateMap<User, UserViewModel>()
                .ForMember(des => des.Role, mem => mem.MapFrom(src => unitOfWork.Settings.GetRolesByUserId(src.Id)))
                .ForMember(des => des.Address, mem => mem.MapFrom(src => unitOfWork.Settings.GetDefaultAddressByUserId(src.Id)))
                .ForMember(des => des.Avatar, mem => mem.MapFrom(src => src.Image));

            //// User
            CreateMap<Request, RequestManageViewModel>()
                .ForMember(des => des.User, mem => mem.MapFrom(src => src.User.UserName));
        }
    }
}