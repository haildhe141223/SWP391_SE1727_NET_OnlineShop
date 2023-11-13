using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Emails
{
    public class EmailContactViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public MailStatus MailStatus { get; set; }
        public string Body { get; set; }    
    }
}
