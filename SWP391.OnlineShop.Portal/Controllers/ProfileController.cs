using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Common.Extensions;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AccountModels;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.ProfileModels;

namespace SWP391.OnlineShop.Portal.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IJsonServiceClient _client;

        public ProfileController(
            UserManager<User> userManager,
            IJsonServiceClient client)
        {
            _userManager = userManager;
            _client = client;
        }

        #region Commons

        private bool CheckUserRequest(User userChange)
        {
            var emailUserRequest = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            return emailUserRequest != null && emailUserRequest.Equals(userChange.Email);
        }

        #endregion

        #region General

        public async Task<IActionResult> Index(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var isCheckUser = CheckUserRequest(user);

            if (!isCheckUser)
            {
                return RedirectToAction("ErrorForbidden", "Account");
            }

            var generalViewModel = new GeneralViewModel
            {
                Id = user.Id.ToString(),
                DisplayEmail = user.Email,
                DisplayImage = user.Image,
                DisplayName = user.UserName,
                DisplayGender = user.Gender,
                DisplayPhoneNumber = user.PhoneNumber
            };

            var profileVm = new ProfileViewModels
            {
                GeneralViewModel = generalViewModel,
                SecurityViewModel = new SecurityViewModel(),
            };

            return View(profileVm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeName(ProfileViewModels request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (string.IsNullOrEmpty(request.GeneralViewModel.DisplayName.Trim()))
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "Display name is empty or null. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            if (user.UserName.Equals(request.GeneralViewModel.DisplayName.Trim()))
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "New display name input is similar with old one. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var checkUser = await _userManager.FindByNameAsync(request.GeneralViewModel.DisplayName.Trim());
            if (checkUser == null)
            {
                var isUpdateName = await _client.PutAsync(new PutUpdateUserName
                {
                    UserId = user.Id,
                    NewName = request.GeneralViewModel.DisplayName.Trim()
                });

                if (isUpdateName.StatusCode != Common.Enums.StatusCode.Success)
                {
                    TempData["IsError"] = "true";
                    TempData["ErrorMess"] = $"{isUpdateName.ErrorMessage}. Please re-enter";
                    return RedirectToAction(nameof(Index), new { userId = user.Id });
                }

                TempData["SuccessMess"] = "Update display name success. Please double-check";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["IsError"] = "true";
            TempData["ErrorMess"] = "This name already taken. Please re-enter";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ProfileViewModels request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (string.IsNullOrEmpty(request.GeneralViewModel.DisplayEmail.Trim()))
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "Display email is empty or null. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            if (!request.GeneralViewModel.DisplayEmail.IsEmail())
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "Data input does not match with email template. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var checkUser = await _userManager.FindByEmailAsync(request.GeneralViewModel.DisplayEmail.Trim());

            if (checkUser == null)
            {
                var isUpdateEmail = await _client.PutAsync(new PutUpdateUserEmail
                {
                    UserId = user.Id,
                    NewEmail = request.GeneralViewModel.DisplayEmail.Trim()
                });

                if (isUpdateEmail.StatusCode != Common.Enums.StatusCode.Success)
                {
                    TempData["IsError"] = "true";
                    TempData["ErrorMess"] = $"{isUpdateEmail.ErrorMessage}. Please re-enter";
                    return RedirectToAction(nameof(Index), new { userId = user.Id });
                }

                TempData["SuccessMess"] = "Update display email success. Please double-check";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["IsError"] = "true";
            TempData["ErrorMess"] = "This email already taken. Please re-enter";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAvatar(IFormFile? updateAvatar)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (updateAvatar == null)
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "File update empty or null. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var imageFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\users\\{user.Id}";
            if (!Directory.Exists(imageFolderLink))
            {
                Directory.CreateDirectory(imageFolderLink);
            }

            var avatarLink = Path.Combine(imageFolderLink, updateAvatar.FileName);

            await using Stream fileStream = new FileStream(avatarLink, FileMode.Create);
            await updateAvatar.CopyToAsync(fileStream);
            var imageLink = $"~/uploads/users/{user.Id}/{updateAvatar.FileName}";

            var isUpdateName = await _client.PutAsync(new PutUpdateUserAvatar
            {
                UserId = user.Id,
                NewAvatar = imageLink
            });

            if (isUpdateName.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = $"{isUpdateName.ErrorMessage}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessMess"] = "Update avatar success. Please double-check";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeGender(ProfileViewModels request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (request.GeneralViewModel.DisplayGender < 0)
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "Display gender is not valid. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var isUpdateGender = await _client.PutAsync(new PutUpdateUserGender
            {
                UserId = user.Id,
                NewGender = request.GeneralViewModel.DisplayGender
            });

            if (isUpdateGender.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = $"{isUpdateGender.ErrorMessage}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessMess"] = "Update display gender success. Please double-check";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhone(ProfileViewModels request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (string.IsNullOrEmpty(request.GeneralViewModel.DisplayPhoneNumber.Trim()))
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "Display phone is empty or null. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            if (!request.GeneralViewModel.DisplayEmail.IsPhone())
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = "Data input does not match with phone template. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var isUpdatePhone = await _client.PutAsync(new PutUpdateUserPhone
            {
                UserId = user.Id,
                NewPhone = request.GeneralViewModel.DisplayPhoneNumber.Trim()
            });

            if (isUpdatePhone.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsError"] = "true";
                TempData["ErrorMess"] = $"{isUpdatePhone.ErrorMessage}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessMess"] = "Update display phone success. Please double-check";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        public async Task<IActionResult> DeleteAccount(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var isCheckUser = CheckUserRequest(user);

            if (!isCheckUser)
            {
                return RedirectToAction("ErrorForbidden", "Account");
            }

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

            return RedirectToAction("Logout", "Account");
        }

        #endregion

        #region Security

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ProfileViewModels request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var password = request.SecurityViewModel.CurrentPassword.Trim();
            var newPassword = request.SecurityViewModel.NewPassword.Trim();
            var retypePassword = request.SecurityViewModel.RePassword.Trim();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", ", errors)}";

                TempData["IsPassError"] = "true";
                TempData["ErrorPassMess"] = $"{modelError}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var isPassword = await _userManager.CheckPasswordAsync(user, password);
            if (isPassword)
            {
                if (retypePassword.Equals(newPassword))
                {
                    var isChangePass = await _userManager.CheckPasswordAsync(user, newPassword);
                    if (isChangePass)
                    {
                        TempData["SuccessPassMess"] = "Change password success. Now you can login with new password.";
                        return RedirectToAction(nameof(Index), new { userId = user.Id });
                    }

                    TempData["IsPassError"] = "true";
                    TempData["ErrorPassMess"] = "Change password fail. Please re-check.";
                    return RedirectToAction(nameof(Index), new { userId = user.Id });
                }

                TempData["IsPassError"] = "true";
                TempData["ErrorPassMess"] = "Retype password does not match with New password. Please re-check.";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["IsPassError"] = "true";
            TempData["ErrorPassMess"] = "Current password does not match. Please re-enter.";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        #endregion
    }
}
