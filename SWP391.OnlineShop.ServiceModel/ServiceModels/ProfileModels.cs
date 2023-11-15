using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;
using SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels;

public class ProfileModels
{
    [Route("/Profile/PutUpdateUserName", "PUT")]
    public class PutUpdateUserName : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public string NewName { get; set; }
    }

    [Route("/Profile/PutUpdateUserEmail", "PUT")]
    public class PutUpdateUserEmail : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public string NewEmail { get; set; }
    }

    [Route("/Profile/PutUpdateUserAvatar", "PUT")]
    public class PutUpdateUserAvatar : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public string NewAvatar { get; set; }
    }

    [Route("/Profile/PutUpdateUserPhone", "PUT")]
    public class PutUpdateUserPhone : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public string NewPhone { get; set; }
    }

    [Route("/Profile/PutUpdateUserGender", "PUT")]
    public class PutUpdateUserGender : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public Gender NewGender { get; set; }
    }

    [Route("/Profile/PutUpdateDefaultAddress", "PUT")]
    public class PutUpdateDefaultAddress : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }

    [Route("/Profile/PutUpdateUserAddress", "PUT")]
    public class PutUpdateUserAddress : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public string AddressId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
    }

    [Route("/Profile/DeleteUserAddress", "DELETE")]
    public class DeleteUserAddress : IReturn<BaseResultModel>
    {
        public int AddressId { get; set; }
    }

    [Route("/Profile/GetUserVouchers", "GET")]
    public class GetUserVouchers : IReturn<List<UserVoucherViewModel>>
    {
        public int UserId { get; set; }
    }

    [Route("/Profile/GetUserAddresses", "GET")]
    public class GetUserAddresses : IReturn<List<AddressViewModel>>
    {
        public int UserId { get; set; }
    }

    [Route("/Profile/PostAddUserAddress", "POST")]
    public class PostAddUserAddress : IReturn<BaseResultModel>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
    }
}