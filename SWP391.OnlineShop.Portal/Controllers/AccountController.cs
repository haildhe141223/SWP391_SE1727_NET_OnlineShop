using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ViewModels.Accounts;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AccountModels;

namespace SWP391.OnlineShop.Portal.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILoggerService _logger;
        private readonly IJsonServiceClient _client;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILoggerService logger,
            IJsonServiceClient client)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _client = client;
        }

        #region Login

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return RedirectToAction("Index", "Home");
            }

            var externalLoginProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            ViewData["ExternalLoginProvider"] = externalLoginProviders;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", ", errors)}";
                _logger.LogError($"Login Error - Model Invalid: {modelError}");
                return StatusCode(500, "Please enter email or password.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user.LockoutEnabled)
            {
                return StatusCode(500, $"User with email {request.Email} does not exist or your account got locked. " +
                                       "Please contact admin for more information");
            }
            if (!user.EmailConfirmed)
            {
                return StatusCode(500, $"Your email {request.Email} did not got confirm yet. " +
                                       "Please contact admin for more information");
            }

            var apiResult = await _client.GetAsync(new GetLogin { Email = request.Email });

            switch (apiResult.StatusCode)
            {
                case Common.Enums.StatusCode.Success:
                    var result = await _signInManager.PasswordSignInAsync(
                        user,
                        request.Password,
                        request.IsPersistent,
                        true);

                    if (result.IsLockedOut)
                    {
                        return StatusCode(500, "User has no permission to access into the system");
                    }

                    if (!result.Succeeded || result.IsNotAllowed)
                    {
                        return StatusCode(500, "Login fail. Found some problems while logging in. You might enter wrong password");
                    }

                    return Ok();
                case Common.Enums.StatusCode.InternalServerError:
                default:
                    return RedirectToAction("Error", "Home");
            }
        }

        #endregion

        #region External Login

        public IActionResult LoginExternally(string request)
        {
            _logger.LogInfo("LoginExternally - Start");

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(request, null);
            properties.RedirectUri = Url.Action("ExternalLoginCallback", "Account");
            _logger.LogInfo($"LoginExternally - {string.Join(", ", properties.Items.Keys)} - " +
                            $"{string.Join(", ", properties.Items.Values)}");

            _logger.LogInfo("LoginExternally - End");

            return Challenge(properties, request);
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            _logger.LogInfo("ExternalLoginCallback - Start");
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();

            var externalEmail = loginInfo.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var externalUsername = loginInfo.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            _logger.LogInfo($"Step 1 - ExternalLoginCallback - Email={externalEmail?.Value}");

            if (externalEmail == null) return RedirectToAction("Error", "Home");

            var userExist = await _userManager.FindByEmailAsync(externalEmail.Value);

            if(userExist == null)
            {
                return StatusCode(500, $"Your email {externalEmail.Value} does not exist " +
                                       "Please contact admin for more information");
            }

            if (userExist.LockoutEnabled)
            {
                _logger.LogInfo($"Step 1.2 - Account with email [{userExist.Email}] already got lock.");
                return RedirectToAction("ErrorForbidden", "Account");
            }
            if (!userExist.EmailConfirmed)
            {
                return StatusCode(500, $"Your email {userExist.Email} did not got confirm yet. " +
                                       "Please contact admin for more information");
            }

            // External login if account existed
            var isExternalInfoExist = await _client.GetAsync(new GetUser
            {
                Email = externalEmail.Value,
                ProviderKey = loginInfo.ProviderKey
            });

            _logger.LogInfo("Step 2 - ExternalLoginCallback");

            if (isExternalInfoExist.StatusCode == Common.Enums.StatusCode.Success)
            {
                var user = await _userManager.FindByEmailAsync(externalEmail.Value);

                if (user.LockoutEnabled)
                {
                    _logger.LogInfo($"Step 2.2 - Account with email [{user.Email}] already got lock.");
                    return RedirectToAction("ErrorForbidden", "Account");
                }

                var isExternalLoginCreated = await _userManager.AddLoginAsync(user, loginInfo);

                if (!isExternalLoginCreated.Succeeded)
                {
                    _logger.LogError(
                        "Error in ExternalLoginCallback request: " +
                        $"{string.Join(", ", isExternalLoginCreated.Errors.Select(e => e.Description))}");
                }
            }

            _logger.LogInfo("Step 3 - ExternalLoginCallback");

            var externalLoginResult = await _client.GetAsync(new GetExternalLogin
            {
                ExternalEmail = externalEmail.Value,
                Username = externalUsername?.Value,
                LoginProvider = loginInfo.LoginProvider,
                ProviderDisplayName = loginInfo.ProviderDisplayName,
                ProviderKey = loginInfo.ProviderKey
            });

            _logger.LogInfo("Step 4 - ExternalLoginCallback");

            _logger.LogInfo($"ExternalLoginCallback - result = {externalLoginResult.StatusCode}");

            switch (externalLoginResult.StatusCode)
            {
                case Common.Enums.StatusCode.Success:
                    var externalResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider,
                    loginInfo.ProviderKey, false);

                    if (externalResult.IsLockedOut)
                    {
                        return RedirectToAction("ErrorForbidden", "Account");
                    }

                    if (!externalResult.Succeeded || externalResult.IsNotAllowed)
                    {
                        return RedirectToAction("ErrorForbidden", "Account");
                    }

                    _logger.LogInfo("ExternalLoginCallback - End");

                    return RedirectToAction("Index", "Home", new { loginRequest = "LOGIN" });
                case Common.Enums.StatusCode.Forbidden:
                    return RedirectToAction("ErrorForbidden", "Account");
                case Common.Enums.StatusCode.InternalServerError:
                default:
                    return RedirectToAction("ErrorInternalServer", "Account");
            }
        }

        #endregion

        #region Register

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return RedirectToAction("Index", "Home");
            }

            var externalLoginProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            ViewData["ExternalLoginProvider"] = externalLoginProviders;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", ", errors)}";
                _logger.LogError($"Register Error - Model Invalid: {modelError}");
                return StatusCode(500, "Invalid data. Please input data while register an account.");
            }

            var userExist = await _userManager.FindByEmailAsync(request.Email);
            if (userExist != null)
            {
                return StatusCode(500, $"User with email [{request.Email}] already exist. " +
                    "Please use other email.");
            }

            var registerResult = await _client.PostAsync(new PostRegisterAccount
            {
                RegisterViewModel = request,
            });

            if (registerResult.StatusCode == Common.Enums.StatusCode.InternalServerError)
            {
                return StatusCode(500, registerResult.ErrorMessage);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return StatusCode(500, $"Success register account. Fail to find user with email [{request.Email}]. " +
                    "Please contact admin or support team.");
            }

            var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var linkConfirmEmail = Url.Action(
                protocol: HttpContext.Request.Scheme,
                host: HttpContext.Request.Host.Value,
                controller: "Account",
                action: "ConfirmEmailRegister",
                values: new { userId = user.Id, emailToken = emailConfirmToken }
            );

            //TODO: HaiLD update send mail here
            var createEmail = await _client.PostAsync(new PostConfirmRegisterEmail
            {
                Category = EmailConstraints.EmailNotificationCategory,
                LinkConfirmPassword = linkConfirmEmail,
                To = request.Email
            });

            if (createEmail.StatusCode == Common.Enums.StatusCode.Success)
            {
                return Ok("Register success. Please check your email address to confirm your email.");
            }

            _logger.LogError($"Register Error - Fail while sending email confirm: {createEmail.ErrorMessage}");
            return StatusCode(500, "Register fail while sending email confirm. Contact admin or support team for more information.");
        }

        #endregion

        #region Passwords

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return StatusCode((int)Common.Enums.StatusCode.NotFound, "Please enter email required.");
            }

            var email = request.Email.Trim().ToUpper();

            var userExist = _userManager.Users.FirstOrDefault(u => u.NormalizedEmail == email);
            if (userExist == null)
            {
                return StatusCode((int)Common.Enums.StatusCode.NotFound,
                    $"Email [{request.Email}] does not exist!. Please recheck or contact admin.");
            }

            var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(userExist);
            var linkResetPassword = Url.Action(
                protocol: HttpContext.Request.Scheme,
                host: HttpContext.Request.Host.Value,
                controller: "Account",
                action: "ConfirmResetPassword",
                values: new { userId = userExist.Id, resetPasswordToken = resetPassToken }
            );

            var api = await _client.PostAsync(new PostConfirmChangePasswordEmail
            {
                Category = EmailConstraints.EmailNotificationCategory,
                LinkResetPassword = linkResetPassword,
                To = request.Email
            });

            if (api.StatusCode == Common.Enums.StatusCode.Created)
                return StatusCode((int)Common.Enums.StatusCode.Success,
                    "Email Sent. Please recheck your mailbox after 5-10 minutes");

            return StatusCode((int)Common.Enums.StatusCode.InternalServerError,
                "Found some error while sending reset-email, Please contact admin to support.");
        }

        public async Task<IActionResult> ConfirmResetPassword(string userId, string resetPasswordToken)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["ErrorMess"] = "User relate to this email does not exist";
                return RedirectToAction("Login");
            }

            var resetPassVm = new ResetPasswordViewModel
            {
                UserId = userId,
                ResetToken = resetPasswordToken
            };

            return View(resetPassVm);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassword(ResetPasswordViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                var modelError = $"{string.Join(", ", errors)}";
                return StatusCode((int)Common.Enums.StatusCode.InternalServerError, modelError);
            }

            if (request.RePassword != request.NewPassword)
            {
                return StatusCode((int)Common.Enums.StatusCode.InternalServerError,
                    "RePassword does not match with NewPassword. Please recheck.");
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return StatusCode((int)Common.Enums.StatusCode.InternalServerError,
                    "User does not exist, Please contact admin for more info.");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);
            if (result.Succeeded)
            {
                return StatusCode((int)Common.Enums.StatusCode.Success,
                    "Change password successful. Please back to login.");
            }

            return StatusCode((int)Common.Enums.StatusCode.InternalServerError,
                $"Found some error while reset your password - {string.Join(", ", result.Errors.Select(e => e.Description))}, " +
                "Please contact admin to support.");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        #endregion

        #region Email
        public async Task<IActionResult> ConfirmEmailRegister(string userId, string emailToken)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["ErrorMess"] = "User relate to this email does not exist";
                return RedirectToAction("Login");
            }

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);
            if (!result.Succeeded)
            {
                TempData["ErrorMess"] = "Failed while validating user email - " +
                                        $"{string.Join(", ", result.Errors.Select(x => x.Description))}";
                return RedirectToAction("Login");
            }

            TempData["SuccessMess"] = "Confirm email address is successfully, " +
                                      "now you can login with your account";
            return RedirectToAction("Login");
        }
        #endregion

        #region Logout

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Error Handler
        [AllowAnonymous]
        public IActionResult Denied()
        {
            return Content("Denied Request.");
        }

        public IActionResult ErrorNotFound()
        {
            return NotFound("404 Not Found");
        }

        public IActionResult ErrorForbidden()
        {
            return Content("403 Error Forbidden");
        }

        public IActionResult ErrorInternalServer()
        {
            return Content("500 Error InternalServer");
        }
        #endregion
    }
}
