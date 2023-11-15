using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Common.Enums;
using SWP391.OnlineShop.Common.Extensions;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceModel.ViewModels.Profiles;
using SWP391.OnlineShop.ServiceModel.ViewModels.Vouchers;
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

            // Voucher
            var vouchers = await _client.GetAsync(new GetUserVouchers
            {
                UserId = user.Id
            });

            // Address
            var addresses = await _client.GetAsync(new GetUserAddresses
            {
                UserId = user.Id
            });

            // Collaboration
            var requests = await _client.GetAsync(new GetUserRequests
            {
                UserId = user.Id
            });

            var profileVm = new ProfileViewModels
            {
                GeneralViewModel = generalViewModel,
                SecurityViewModel = new SecurityViewModel(),
                AddressViewModel = new AddressViewModels
                {
                    AddressViewModelList = addresses
                },
                VoucherViewModels = vouchers ?? new List<UserVoucherViewModel>(),
                RequestViewModel = new RequestViewModels
                {
                    RequestDataViewModels = requests
                }
            };

            // Request Tab
            var requestTabString = TempData["RequestTab"] as string ?? string.Empty;

            if (!string.IsNullOrEmpty(requestTabString))
            {
                var requestTab = requestTabString.ToEnum<ProfileTab>();
                profileVm.RequestTab = requestTab;
            }
            else
            {
                var requestTab = HttpContext.Session.GetString("RequestTab");
                if (!string.IsNullOrEmpty(requestTab))
                {
                    var requestTabSession = requestTab.ToEnum<ProfileTab>();
                    profileVm.RequestTab = requestTabSession;
                    HttpContext.Session.Remove("RequestTab");
                }
            }

            return View(profileVm);
        }

        #region General

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
            var imageLink = $"/uploads/users/{user.Id}/{updateAvatar.FileName}";

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
            if (request.SecurityViewModel.ProfileTab == ProfileTab.Security)
            {
                TempData["RequestTab"] = ProfileConstraints.SecurityTab;
            }

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", </br>", errors)}";

                TempData["IsPassError"] = "true";
                TempData["ErrorPassMess"] = $"{modelError}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var password = request.SecurityViewModel.CurrentPassword.Trim();
            var newPassword = request.SecurityViewModel.NewPassword.Trim();
            var retypePassword = request.SecurityViewModel.RePassword.Trim();

            var isPassword = await _userManager.CheckPasswordAsync(user, password);
            if (isPassword)
            {
                if (retypePassword.Equals(newPassword))
                {
                    var isChangePass = await _userManager.ChangePasswordAsync(user, password, newPassword);
                    if (isChangePass.Succeeded)
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

        #region Address

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddress(ProfileViewModels request)
        {
            if (request.AddressViewModel.ProfileTab == ProfileTab.Address)
            {
                TempData["RequestTab"] = ProfileConstraints.AddressTab;
            }

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", </br>", errors)}";

                TempData["IsAddressError"] = "true";
                TempData["ErrorAddressMess"] = $"{modelError}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var address = request.AddressViewModel.Address.Trim().ReplaceSpecialCharacters();
            var fullName = request.AddressViewModel.FullName.Trim().ReplaceSpecialCharacters();
            var phone = request.AddressViewModel.PhoneNumber.Trim().ReplaceSpecialCharacters();
            var isDefault = request.AddressViewModel.IsDefault;

            var isAddAddress = await _client.PostAsync(new PostAddUserAddress
            {
                PhoneNumber = phone,
                Address = address,
                FullName = fullName,
                IsDefault = isDefault,
                UserId = user.Id
            });

            if (isAddAddress.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsAddError"] = "true";
                TempData["ErrorAddMess"] = $"Error while adding address. {isAddAddress.ErrorMessage}.";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessAddressMess"] = "Adding address success. Please double-check";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        public async Task<IActionResult> SetAsDefault(int ud, int id)
        {
            TempData["RequestTab"] = ProfileConstraints.AddressTab;

            var user = await _userManager.FindByIdAsync(ud.ToString());
            if (user == null)
            {
                return RedirectToAction("ErrorForbidden", "Account");
            }

            var isCheckUser = CheckUserRequest(user);
            if (!isCheckUser)
            {
                return RedirectToAction("ErrorForbidden", "Account");
            }

            var isSetDefault = await _client.PutAsync(new PutUpdateDefaultAddress
            {
                AddressId = id,
                UserId = user.Id
            });

            if (isSetDefault.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsAddError"] = "true";
                TempData["ErrorAddMess"] = $"Error while set default address. {isSetDefault.ErrorMessage}.";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessAddressMess"] = "Set default address success. Please double-check";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        public async Task<IActionResult> DeleteAddress(int ud, int id)
        {
            TempData["RequestTab"] = ProfileConstraints.AddressTab;

            var user = await _userManager.FindByIdAsync(ud.ToString());
            if (user == null)
            {
                return RedirectToAction("ErrorForbidden", "Account");
            }

            var isCheckUser = CheckUserRequest(user);
            if (!isCheckUser)
            {
                return RedirectToAction("ErrorForbidden", "Account");
            }

            var isDeleteAddress = await _client.DeleteAsync(new DeleteUserAddress
            {
                AddressId = id
            });

            if (isDeleteAddress.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsAddError"] = "true";
                TempData["ErrorAddMess"] = $"Error while delete address. {isDeleteAddress.ErrorMessage}.";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessAddressMess"] = "Delete address success. Please double-check";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        public async Task<IActionResult> UpdateAddress(AddressViewModels request)
        {

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", </br>", errors)}";
                var errorMess = $"{modelError}. Please re-enter";
                return StatusCode((int)Common.Enums.StatusCode.InternalServerError, errorMess);
            }

            var addressId = request.Id;
            var address = request.Address.Trim().ReplaceSpecialCharacters();
            var fullName = request.FullName.Trim().ReplaceSpecialCharacters();
            var phone = request.PhoneNumber.Trim().ReplaceSpecialCharacters();
            var isDefault = request.IsDefault;

            var isUpdateAddress = await _client.PutAsync(new PutUpdateUserAddress
            {
                UserId = user.Id,
                AddressId = addressId,
                PhoneNumber = phone,
                Address = address,
                FullName = fullName,
                IsDefault = isDefault
            });

            if (isUpdateAddress.StatusCode != Common.Enums.StatusCode.Success)
            {
                var errorMess = $"Error while update address. {isUpdateAddress.ErrorMessage}.";
                return StatusCode((int)Common.Enums.StatusCode.InternalServerError, errorMess);
            }

            HttpContext.Session.SetString("RequestTab", ProfileConstraints.AddressTab);
            var successMess = "Update address success. Please double-check.";
            return Ok(successMess);
        }

        #endregion

        #region Collaboration

        [HttpPost]
        public async Task<IActionResult> RequestToBecomeMarketer(ProfileViewModels request)
        {
            if (request.RequestViewModel.ProfileTab == ProfileTab.Collaboration)
            {
                TempData["RequestTab"] = ProfileConstraints.CollaborationTab;
            }

            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.Name");
            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.Email");
            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.Reason");
            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.FullAddress");
            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.PhoneNumber");
            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.BackOfIdentityCard");
            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.FrontOfIdentityCard");
            ModelState.Remove("RequestViewModel.RequestSaleManagerViewModel.BusinessRegistrationCertificate");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", </br>", errors)}";

                TempData["IsCollaborationError"] = "true";
                TempData["ErrorCollaborationMess"] = $"{modelError}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var name = request.RequestViewModel.RequestMarketingViewModel.Name;
            var email = request.RequestViewModel.RequestMarketingViewModel.Email.Trim();
            var phone = request.RequestViewModel.RequestMarketingViewModel.Phone.Trim();
            var author = request.RequestViewModel.RequestMarketingViewModel.Author.Trim();
            var samplePostLink = request.RequestViewModel.RequestMarketingViewModel.SamplePostLink;

            var isRequestMarketer = await _client.PostAsync(new PostBecomeRequestMarketer
            {
                UserId = user.Id,
                PhoneNumber = phone,
                FullName = name,
                Author = author,
                Email = email,
                SamplePostLink = samplePostLink
            });

            if (isRequestMarketer.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsCollaborationError"] = "true";
                TempData["ErrorCollaborationMess"] = $"Error while request to become blogger/marketer. {isRequestMarketer.ErrorMessage}.";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessCollaborationMess"] = "Request to become blogger/marketer. Admin will view and report back later.";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> RequestToBecomeSaleManager(ProfileViewModels request)
        {
            if (request.RequestViewModel.ProfileTab == ProfileTab.Collaboration)
            {
                TempData["RequestTab"] = ProfileConstraints.CollaborationTab;
            }

            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            ModelState.Remove("RequestViewModel.RequestMarketingViewModel.Name");
            ModelState.Remove("RequestViewModel.RequestMarketingViewModel.Email");
            ModelState.Remove("RequestViewModel.RequestMarketingViewModel.Phone");
            ModelState.Remove("RequestViewModel.RequestMarketingViewModel.Author");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", </br>", errors)}";

                TempData["IsCollaborationError"] = "true";
                TempData["ErrorCollaborationMess"] = $"{modelError}. Please re-enter";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            var name = request.RequestViewModel.RequestSaleManagerViewModel.Name.Trim();
            var email = request.RequestViewModel.RequestSaleManagerViewModel.Email.Trim();
            var phone = request.RequestViewModel.RequestSaleManagerViewModel.PhoneNumber.Trim();
            var address = request.RequestViewModel.RequestSaleManagerViewModel.FullAddress.Trim().ReplaceSpecialCharacters();
            var reason = request.RequestViewModel.RequestSaleManagerViewModel.Reason.Trim().ReplaceSpecialCharacters();
            var businessCertificate = request.RequestViewModel.RequestSaleManagerViewModel.BusinessRegistrationCertificate;
            var frontOfIdentityCard = request.RequestViewModel.RequestSaleManagerViewModel.FrontOfIdentityCard;
            var backOfIdentityCard = request.RequestViewModel.RequestSaleManagerViewModel.BackOfIdentityCard;

            var identityCardFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\users\\{user.Id}\\identities\\card";
            if (!Directory.Exists(identityCardFolderLink))
            {
                Directory.CreateDirectory(identityCardFolderLink);
            }

            var userCertificateFolderLink = $"{Convert.ToString(Directory.GetCurrentDirectory())}\\wwwroot\\uploads\\users\\{user.Id}\\identities\\certificate";
            if (!Directory.Exists(userCertificateFolderLink))
            {
                Directory.CreateDirectory(userCertificateFolderLink);
            }

            var businessCertificateLink = Path.Combine(userCertificateFolderLink, businessCertificate.FileName);
            await using Stream fileStream = new FileStream(businessCertificateLink, FileMode.Create);
            await businessCertificate.CopyToAsync(fileStream);
            var businessCertificateSaveLink = $"/uploads/users/{user.Id}/identities/certificate/{businessCertificate.FileName}";

            var frontOfIdentityCardLink = Path.Combine(identityCardFolderLink, frontOfIdentityCard.FileName);
            await using Stream fileStreamFrontCard = new FileStream(frontOfIdentityCardLink, FileMode.Create);
            await frontOfIdentityCard.CopyToAsync(fileStreamFrontCard);
            var frontOfIdentityCardSaveLink = $"/uploads/users/{user.Id}/identities/card/{frontOfIdentityCard.FileName}";

            var backOfIdentityCardLink = Path.Combine(identityCardFolderLink, backOfIdentityCard.FileName);
            await using Stream fileStreamBackCard = new FileStream(backOfIdentityCardLink, FileMode.Create);
            await backOfIdentityCard.CopyToAsync(fileStreamBackCard);
            var backOfIdentityCardSaveLink = $"/uploads/users/{user.Id}/identities/card/{backOfIdentityCard.FileName}";

            var isRequestSaleManager = await _client.PostAsync(new PostBecomeRequestSaleManager
            {
                UserId = user.Id,
                FullAddress = address,
                FullName = name,
                Email = email,
                BusinessCertificateLink = businessCertificateSaveLink,
                FrontOfIdentityCardLink = frontOfIdentityCardSaveLink,
                BackOfIdentityCardLink = backOfIdentityCardSaveLink,
                PhoneNumber = phone,
                Reason = reason
            });

            if (isRequestSaleManager.StatusCode != Common.Enums.StatusCode.Success)
            {
                TempData["IsCollaborationError"] = "true";
                TempData["ErrorCollaborationMess"] = $"Error while request to become sale/sale manager. {isRequestSaleManager.ErrorMessage}.";
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            TempData["SuccessCollaborationMess"] = "Request to become sale/sale manager. Admin will view and report back later.";
            return RedirectToAction(nameof(Index), new { userId = user.Id });
        }

        #endregion
    }
}
