using ServiceStack;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;

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
}