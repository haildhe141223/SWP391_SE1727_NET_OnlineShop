using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Feedback
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public decimal RatedStar { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
    }
}
