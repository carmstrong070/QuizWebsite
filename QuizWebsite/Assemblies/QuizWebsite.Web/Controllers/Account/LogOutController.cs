using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;

namespace QuizWebsite.Web.Controllers.Account
{
    [Area("Account")]
    public class LogOutController : AuthenticatedControllerBase
    {
        public LogOutController(IUserManager authUserManager) : base(authUserManager) { }

        [HttpGet]
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            AuthUserManager.LogOut(HttpContext);

            return RedirectToAction("QuizPortal", "QuizPortal", new { area = "QuizGame" });
        }

    }
}
