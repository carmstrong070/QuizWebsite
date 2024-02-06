using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;

namespace QuizWebsite.Web.Controllers
{
    public class LogOutController : Controller
    {
        protected IUserManager AuthUserManager { get; set; }

        public LogOutController(IUserManager authUserManager)
        {
            AuthUserManager = authUserManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult LogOut()
        {
            AuthUserManager.LogOut(this.HttpContext);

            return RedirectToAction("QuizPortal", "QuizPortal");
        }

    }
}
