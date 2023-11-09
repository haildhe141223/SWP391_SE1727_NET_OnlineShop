using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Feedback
{
    public class FeedbackViewModels
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public decimal RatedStar { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
