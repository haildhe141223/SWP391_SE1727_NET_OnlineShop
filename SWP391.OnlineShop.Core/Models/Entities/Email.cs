using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Email : BaseEntity<int>
{
    public string To { get; set; }
    public string Cc { get; set; }
    public string Bcc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Category { get; set; }
    public string Title { get; set; }
    public string ErrorMessage { get; set; }
    public MailStatus MailStatus { get; set; }
    public int RetryCounter { get; set; }
    public string CreatedBy { get; set; }
}