using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models;

namespace QuizWebsite.Web.Controllers
{
    public class UserStatsController : AuthenticatedControllerBase
    {
        public UserStatsController(IUserManager authUserManager) : base(authUserManager) { }

        [HttpGet]
        public IActionResult UserStats()
        {
            var userStatsViewModel = new UserStatsViewModel();
            userStatsViewModel.TotalTimeQuizzing = TheAuditor.GetTotalTimeQuizzing(UserId.Value);
            return View("UserStats", userStatsViewModel);
        }
    }
}
