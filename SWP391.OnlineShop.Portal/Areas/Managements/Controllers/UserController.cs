using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Common.Extensions;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ViewModels.Users;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AccountModels;

namespace SWP391.OnlineShop.Portal.Areas.Managements.Controllers
{
    [Authorize(Roles = RoleConstraints.Admin)]
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;
        private readonly IJsonServiceClient _client;

        public UserController(
            UserManager<User> userManager,
            ILoggerService logger,
            IJsonServiceClient client)
        {
            _userManager = userManager;
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var userCount = _userManager.Users.Count();

            var users = await _client.GetAsync(new GetUsers
            {
                Size = userCount,
                LockoutEnabled = false
            });

            return View(users);
        }

        public async Task<IActionResult> LockoutUser()
        {
            var userCount = _userManager.Users.Count();

            var users = await _client.GetAsync(new GetUsers
            {
                Size = userCount,
                LockoutEnabled = true
            });

            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var permissionVm = await _client.GetAsync(new GetUserInPermissionTab
            {
                UserId = id
            });

            var user = await _userManager.FindByIdAsync(id);

            var data = new UserDetailViewModels
            {
                UserUpdateId = id,
                PermissionViewModel = permissionVm,
                UserIdentityViewModel = new UserIdentityViewModel
                {
                    Email = user?.Email,
                    Phone = user?.PhoneNumber
                },
                UserSecurityViewModel = new UserSecurityViewModel()
            };

            return View(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var isDeleteAccount = await _client.DeleteAsync(new DeleteUser
            {
                UserId = user.Id
            });

            if (isDeleteAccount.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = $"{isDeleteAccount.ErrorMessage}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessMess"] = $"This user with id - [{id}] have been change status to locked - " +
                                      "Please double check in LockedTab.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Unlock(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var isDeleteAccount = await _client.PutAsync(new PutUnlockUser
            {
                UserId = user.Id
            });

            if (isDeleteAccount.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["ErrorMess"] = $"{isDeleteAccount.ErrorMessage}. Please re-enter";
                return RedirectToAction(nameof(LockoutUser));
            }

            TempData["SuccessMess"] = $"This user with id - [{id}] have been unlock - " +
                                      "Please double-check.";
            return RedirectToAction(nameof(LockoutUser));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel request)
        {
            return View();
        }


        #region Update

        [HttpPost]
        public async Task<IActionResult> UpdateUserPermission(UserDetailViewModels request)
        {
            var imageLink = string.Empty;
            if (request.PermissionViewModel.Avatar != null)
            {
                var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\users\\{request.UserUpdateId}";
                if (!Directory.Exists(imageFolderLink))
                {
                    Directory.CreateDirectory(imageFolderLink);
                }

                var file = request.PermissionViewModel.Avatar;
                var fileName = request.PermissionViewModel.Avatar.FileName;

                var avatarLink = Path.Combine(imageFolderLink, fileName);

                await using Stream fileStream = new FileStream(avatarLink, FileMode.Create);
                await file.CopyToAsync(fileStream);
                imageLink = $"/uploads/users/{request.UserUpdateId}/{fileName}";
            }

            var gender = request.PermissionViewModel.Gender;

            var isUpdatePermission = await _client.PutAsync(new PutUserInPermissionTab
            {
                UserId = request.UserUpdateId,
                Gender = gender,
                RoleViewModels = request.PermissionViewModel.RoleViewModels,
                ImageLink = imageLink
            });

            if (isUpdatePermission.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["ErrorMess"] = $"{isUpdatePermission.ErrorMessage}. Please re-enter";
                return RedirectToAction("Details", new { id = request.UserUpdateId });
            }

            TempData["SuccessMess"] = "Update user success. Please double-check";
            return RedirectToAction("Details", new { id = request.UserUpdateId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserIdentity(UserDetailViewModels request)
        {
            var email = request.UserIdentityViewModel.Email;
            var phone = request.UserIdentityViewModel.Phone;
            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone))
            {
                TempData["ErrorMess"] = "Email and phone is require field. Please re-enter";
                return RedirectToAction("Details", new { id = request.UserUpdateId });
            }

            if (!request.UserIdentityViewModel.Email.IsEmail())
            {
                TempData["ErrorMess"] = "Email enter is invalid. Please re-enter";
                return RedirectToAction("Details", new { id = request.UserUpdateId });
            }

            if (!request.UserIdentityViewModel.Phone.IsPhone())
            {
                TempData["ErrorMess"] = "Phone enter is invalid. Please re-enter";
                return RedirectToAction("Details", new { id = request.UserUpdateId });
            }

            var user = await _userManager.FindByIdAsync(request.UserUpdateId);
            var isUpdate = false;
            if (string.IsNullOrEmpty(user.Email) && !user.Email.Equals(email))
            {
                user.Email = email;
                isUpdate = true;
            }
            if (string.IsNullOrEmpty(user.PhoneNumber) || !user.PhoneNumber.Equals(phone))
            {
                user.PhoneNumber = phone;
                isUpdate = true;
            }

            if (isUpdate)
            {
                var isUpdateResult = await _userManager.UpdateAsync(user);

                if (isUpdateResult.Succeeded)
                {
                    TempData["SuccessMess"] = "Update user success. Please double-check";
                    return RedirectToAction("Details", new { id = request.UserUpdateId });
                }

                TempData["ErrorMess"] = $"{string.Join(", ", isUpdateResult.Errors.Select(x => x.Description).ToList())}. " +
                                        "Please re-enter";
                return RedirectToAction("Details", new { id = request.UserUpdateId });
            }

            return RedirectToAction("Details", new { id = request.UserUpdateId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserSecurity(UserDetailViewModels request)
        {
            var newPass = request.UserSecurityViewModel.NewPassword;
            var retypePass = request.UserSecurityViewModel.RetypePassword;
            if (string.IsNullOrEmpty(newPass) ||
                string.IsNullOrEmpty(retypePass))
            {
                TempData["ErrorMess"] = "New password and Retype password is require field. Please re-enter";
                return RedirectToAction("Details", new { id = request.UserUpdateId });
            }

            if (retypePass.Equals(newPass))
            {
                var user = await _userManager.FindByIdAsync(request.UserUpdateId);
                var changePassToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var isChangePass = await _userManager.ResetPasswordAsync(user, changePassToken, newPass);
                if (isChangePass.Succeeded)
                {
                    TempData["SuccessMess"] = "Update user success. Please double-check";
                    return RedirectToAction("Details", new { id = request.UserUpdateId });
                }
                else
                {
                    TempData["ErrorMess"] = $"{string.Join(", ", isChangePass.Errors.Select(x => x.Description).ToList())}. " +
                                            "Please re-enter";
                    return RedirectToAction("Details", new { id = request.UserUpdateId });
                }
            }

            TempData["ErrorMess"] = "Retype password does not match with New Password. Please re-enter";
            return RedirectToAction("Details", new { id = request.UserUpdateId });
        }
        #endregion
    }
}
