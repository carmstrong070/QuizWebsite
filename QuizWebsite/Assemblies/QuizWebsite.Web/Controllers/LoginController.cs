using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models;

namespace QuizWebsite.Web.Controllers
{
    public class LoginController : Controller
    {
        protected IUserManager AuthUserManager { get; set; }

        public LoginController(IUserManager authUserManager)
        {
            AuthUserManager = authUserManager;
        }

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
                AuthUserManager.SignIn(this.HttpContext, validateResult.User, true);
                return RedirectToAction("QuizPortal", "QuizPortal");
            }

            return View(vm);
        }
    }
}