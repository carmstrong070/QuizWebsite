﻿using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Models;

namespace QuizWebsite.Web.Controllers
{
    public class QuizPortalController : Controller
    {
        [HttpGet]
        [Route("")]
        [Route("Quizzes")]
        public IActionResult QuizPortal()
        {
            var vm = new QuizPortalViewModel();
            vm.Quizzes = QuizGet.GetQuizList();
            vm.AverageCompletionTimes = vm.Quizzes.ToDictionary(x => x.QuizId, x => TheAuditor.GetAverageCompletionTime(x.QuizId));
            return View(vm);
        }
    }
}
