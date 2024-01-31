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
        public IActionResult Index()
        {
            var vm = new IndexModel();
            vm.LoadedQuiz = QuizGet.GetQuiz();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(IndexModel vm)
        {
            vm.LoadedQuiz = QuizGet.GetQuiz();
            var scoringResult = QuizScore.GetNumberCorrect(vm.LoadedQuiz, vm.QuizAnswers);
            vm.IsSubmitted = true;
            vm.CountCorrect = scoringResult.CorrectCount;
            //vm.QuizAnswers = new List<Pages.QuizAnswersViewModel>();
            ModelState.Clear();
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
