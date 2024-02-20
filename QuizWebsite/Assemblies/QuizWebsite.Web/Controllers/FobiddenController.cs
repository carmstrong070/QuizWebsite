using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizWebsite.Web.Controllers
{
    public class FobiddenController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("Forbidden")]
        public IActionResult Forbidden()
        {
            return Redirect("https://www.youtube.com/watch?v=7fW1eWuQkCE");
        }
    }
}
