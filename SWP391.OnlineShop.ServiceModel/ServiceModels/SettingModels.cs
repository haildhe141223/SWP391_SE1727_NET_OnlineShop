using ServiceStack;
using SWP391.OnlineShop.ServiceModel.ViewModels.Settings;

namespace SWP391.OnlineShop.ServiceModel.ServiceModels
{
    public class SettingModels
    {

        [Route("/Setting/GetAllSetting", "GET")]
        public class GetAllSetting : IReturn<List<SettingViewModel>>
        {
        }

        [Route("/Setting/GetSettingById", "GET")]
        public class GetSettingById : IReturn<SettingViewModel>
        {
            public int SettingId { get; set; }
        }

        [Route("/Setting/PostAddSettingt", "POST")]
        public class PostAddSetting : IReturn<SettingViewModel>
        {
            public string Type { get; set; }
            public decimal Value { get; set; }
            public int? OrderId { get; set; }
            public string Status { get; set; }
        }

        [Route("/Setting/PutUpdateSetting", "PUT")]
        public class PutUpdateSetting : IReturn<SettingViewModel>
        {

            public string Type { get; set; }
            public decimal Value { get; set; }
            public int? OrderId { get; set; }
            public string Status { get; set; }
            public int Id { get; set; }
        }

        [Route("/Setting/DeleteSetting", "DELETE")]
        public class DeleteSetting : IReturn<SettingViewModel>
        {
            public int SettingId { get; set; }
        }
    }
}
