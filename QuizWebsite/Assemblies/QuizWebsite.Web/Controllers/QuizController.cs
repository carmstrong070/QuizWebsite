using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Core.Models;
using QuizWebsite.Data;
using QuizWebsite.Web.Models;
using QuizWebsite.Web.Utilities;
using System.Security.Claims;


namespace QuizWebsite.Web.Controllers
{
    public class QuizController : Controller
    {

        [HttpGet]
        [Route("Quiz/{quizId}")]
        public IActionResult Quiz(long quizId)
        {
            var vm = new QuizViewModel();
            vm.LoadedQuiz = QuizHandler.GetQuiz(quizId);
            TempData["start_timestamp"] = DateTime.Now;
            return View(vm);
        }
        [HttpPost]
        [Route("Quiz/{quizId}")]
        public IActionResult Quiz(long quizId, QuizViewModel vm)
        {
            vm.LoadedQuiz = QuizHandler.GetQuiz(quizId);
            var scoringResultsDict = QuizScore.GetNumberCorrect(vm.LoadedQuiz, vm.QuestionResponses);

            var quizAttempt = new QuizAttempt();
            if (this.User.Identity.IsAuthenticated)
            {
                quizAttempt.UserId = long.Parse(this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            }
            quizAttempt.QuizId = quizId;
            quizAttempt.start_timestamp = (DateTime)TempData["start_timestamp"];
            quizAttempt.end_timestamp = DateTime.Now;
            var attemptId = QuizAttemptHandler.Do(quizAttempt);

            var questionResponses = new List<QuestionResponse>();
            for (int i = 0; i < scoringResultsDict.Count(); i++)
            {
                QuestionResponse questionResponse = new QuestionResponse();
                questionResponse.QuizAttemptId = attemptId;
                questionResponse.QuestionId = scoringResultsDict.ElementAt(i).Key;
                questionResponse.AnsweredCorrectly = scoringResultsDict.ElementAt(i).Value;
                questionResponses.Add(questionResponse);
            }
            QuestionResponseHandler.Insert(questionResponses);

            vm.IsSubmitted = true;
            vm.CountCorrect = scoringResultsDict.Count(x => x.Value);
            return View(vm);
        }
    }
}
