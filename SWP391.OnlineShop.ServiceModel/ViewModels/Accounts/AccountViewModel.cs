using SWP391.OnlineShop.ServiceModel.Results;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Accounts
{
    public class AccountViewModel : BaseResultModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
