using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;

namespace QuizWebsite.Web.Controllers
{
    public class LayoutController : AuthenticatedControllerBase
    {
        public LayoutController(IUserManager authUserManager) : base(authUserManager) { }
        public IActionResult Index()
        {
            return View();
        }
    }
}
