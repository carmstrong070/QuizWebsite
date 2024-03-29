﻿using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models.Stats;

namespace QuizWebsite.Web.Controllers.Stats
{
    [Area("Stats")]
    public class UserStatsController : AuthenticatedControllerBase
    {
        public UserStatsController(IUserManager authUserManager) : base(authUserManager) { }

        [HttpGet]
        [Route("Stats")]
        public IActionResult UserStats()
        {
            var userStatsViewModel = new UserStatsViewModel();
            userStatsViewModel.TotalTimeQuizzing = TheAuditor.GetTotalTimeQuizzing(UserId.Value);
            userStatsViewModel.AverageQuizScore = TheAuditor.GetAverageUserQuizScore(UserId.Value);
            userStatsViewModel.OverallQuestionsCorrect = TheAuditor.GetOverallUserQuestionsCorrect(UserId.Value);
            userStatsViewModel.LastQuizCompleted = TheAuditor.GetLastUserQuizTaken(UserId.Value);

            return View("UserStats", userStatsViewModel);
        }
    }
}
