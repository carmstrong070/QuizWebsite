using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;

namespace QuizWebsite.Web.Controllers
{
    public class LogOutController : AuthenticatedControllerBase
    {
        public LogOutController(IUserManager authUserManager) : base(authUserManager) { }

        [HttpGet]
        public IActionResult LogOut()
        {
            AuthUserManager.LogOut(this.HttpContext);

            return RedirectToAction("QuizPortal", "QuizPortal");
        }

    }
}
