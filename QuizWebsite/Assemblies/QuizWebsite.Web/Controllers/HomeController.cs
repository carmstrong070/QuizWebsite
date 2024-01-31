using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Models;
using System.Diagnostics;

namespace QuizWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index(long quizId)
        {
            var vm = new IndexModel();
            // TODO Add logic to determine what quiz id should be passed to .GetQuiz()
            vm.LoadedQuiz = QuizGet.GetQuiz(quizId);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(IndexModel vm)
        {
            // TODO Same deal with .GetQuiz() as above
            vm.LoadedQuiz = QuizGet.GetQuiz(1);
            var scoringResult = QuizScore.GetNumberCorrect(vm.LoadedQuiz, vm.QuizAnswers);
            vm.IsSubmitted = true;
            vm.CountCorrect = scoringResult.CorrectCount;
            //vm.QuizAnswers = new List<Pages.QuizAnswersViewModel>();
            ModelState.Clear();
            return View(vm);
        }
        [HttpGet]
        public IActionResult QuizPortal()
        {
            var vm = new QuizPortalViewModel();
            vm.Quizzes = QuizGet.GetQuizList();
            return View(vm);
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
