using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.ServiceModel.Results;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Emails
{
    public class EmailViewModel : BaseResultModel
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public MailStatus MailStatus { get; set; }
        public int RetryCounter { get; set; }
    }
}
