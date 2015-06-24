using System;
using System.Web.Mvc;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

using OctoBotSharp.Domain;
using OctoBotSharp.Web.Models.Auth;
using OctoBotSharp.Data.Identity;

namespace OctoBotSharp.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly OctoUserManager _userManager;
        private readonly OctoSignInManager _signInManager;

        public AuthController(OctoUserManager userManager, OctoSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(NewUserRegistration newUserModel)
        {
            if (!ModelState.IsValid)
            {
                newUserModel.Password = string.Empty;
                newUserModel.ConfirmPassword = string.Empty;

                return View(newUserModel);
            }

            var user = OctoUser.Create(newUserModel.EmailAddress, newUserModel.Username);
            var createResult = await _userManager.CreateAsync(user, newUserModel.Password);

            if (!createResult.Succeeded)
            {
                AddModelErrors(createResult);
                newUserModel.Password = string.Empty;
                newUserModel.ConfirmPassword = string.Empty;

                return View(newUserModel);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

            var body = "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">Confirm EMail</a>";
            var subject = "OctoBotSharp: Confirm your account";
            await _userManager.SendEmailAsync(user.Id, subject, body);

            return View("Register_Success");
        }

        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
                return View("ConfirmEmail_Success");

            AddModelErrors(result);
            return View("ConfirmEmail_Failure");
        }

        [HttpPost]
        public async Task<ActionResult> Login(PasswordSignIn signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, signInModel.RememberMe, true);

                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Home");

                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "Invalid Username / Password combination");
                        break;

                    case SignInStatus.LockedOut:
                        ModelState.AddModelError("", "Account locked out, contract admin");
                        break;

                    case SignInStatus.RequiresVerification:
                        ModelState.AddModelError("", "Email verification required");
                        break;

                    default:
                        ModelState.AddModelError("", "Unrecognised sign in status");
                        break;
                }
            }

            signInModel.Password = null;
            return View("Index", signInModel);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgottenPassword model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

                var resetUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                var body = "Please reset your password by clicking this link: <a href=\"" + resetUrl + "\">Reset Password</a>";
                var subject = "OctoBotSharp: Reset your password";

                await _userManager.SendEmailAsync(user.Id, subject, body);
            }

            return View("ForgotPassword_EMailSent");
        }

        public ActionResult ResetPassword(int userId, string code)
        {
            var model = new ResetPassword()
            {
                UserId = userId,
                Code = code,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPassword model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userManager.ResetPasswordAsync(model.UserId, model.Code, model.Password);
            if (result.Succeeded)
                return View("Index");

            AddModelErrors(result);
            return View(model);
        }
    }
}
