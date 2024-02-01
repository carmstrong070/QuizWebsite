using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Models;

namespace QuizWebsite.Web.Controllers
{
    public class QuizController : Controller
    {
        [HttpGet]
        [Route("Quiz/{quizId}")]
        public IActionResult Quiz(long quizId)
        {
            var vm = new QuizViewModel();
            // TODO Add logic to determine what quiz id should be passed to .GetQuiz()
            vm.LoadedQuiz = QuizGet.GetQuiz(quizId);
            return View(vm);
        }
        [HttpPost]
        [Route("Quiz/{quizId}")]
        public IActionResult Quiz(long quizId, QuizViewModel vm)
        {
            vm.LoadedQuiz = QuizGet.GetQuiz(quizId);
            var scoringResult = QuizScore.GetNumberCorrect(vm.LoadedQuiz, vm.QuizAnswers);
            vm.IsSubmitted = true;
            vm.CountCorrect = scoringResult.CorrectCount;
            //vm.QuizAnswers = new List<Pages.QuizAnswersViewModel>();
            ModelState.Clear();
            return View(vm);
        }
    }
}
