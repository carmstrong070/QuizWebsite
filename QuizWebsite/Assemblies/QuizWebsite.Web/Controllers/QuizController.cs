using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Models;
using QuizWebsite.Web.Utilities;


namespace QuizWebsite.Web.Controllers
{
    public class QuizController : Controller
    {
        [HttpGet]
        [Route("Quiz/{quizId}")]
        public IActionResult Quiz(long quizId)
        {
            var vm = new QuizViewModel();
            vm.LoadedQuiz = QuizGet.GetQuiz(quizId);
            return View(vm);
        }
        [HttpPost]
        [Route("Quiz/{quizId}")]
        public IActionResult Quiz(long quizId, QuizViewModel vm)
        {
            vm.LoadedQuiz = QuizGet.GetQuiz(quizId);
            var scoringResult = QuizScore.GetNumberCorrect(vm.LoadedQuiz, vm.QuestionResponses);
            vm.IsSubmitted = true;
            vm.CountCorrect = scoringResult.CorrectCount;
            return View(vm);
        }
    }
}
