using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Models;

namespace QuizWebsite.Web.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            var vm = new LoginViewModel();
            return View(vm);
        }
    }
}
