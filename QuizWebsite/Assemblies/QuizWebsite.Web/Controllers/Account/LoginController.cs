using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models.Account;

namespace QuizWebsite.Web.Controllers.Account
{
    [Area("Account")]
    public class LoginController : AuthenticatedControllerBase
    {
        public LoginController(IUserManager authUserManager) : base(authUserManager) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            var vm = new LoginViewModel();
            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel vm)
        {
            ValidateResult validateResult = AuthUserManager.Validate(vm.Username, vm.Password);

            if (validateResult.Success)
            {
                AuthUserManager.SignIn(HttpContext, validateResult.User, true);
                return RedirectToAction("QuizPortal", "QuizPortal", new { area = "QuizGame" });
            }
            else
            {
                if (validateResult.Error == ValidateResultError.GotBanned)
                    return Redirect("https://www.youtube.com/watch?v=kav7tifmyTg");
            }

            return View(vm);
        }
    }
}