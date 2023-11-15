using Microsoft.AspNetCore.Http;
using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.ServiceModel.ViewModels.Users;

public class UserPermissionViewModel
{
    public IFormFile Avatar { get; set; }
    public string AvatarLink { get; set; }
    public Gender Gender { get; set; }
    public List<RoleViewModel> RoleViewModels { get; set; }
}

public class RoleViewModel
{
    public int Id { get; set; }
    public string LabelName { get; set; }
    public bool IsChecked { get; set; }
}