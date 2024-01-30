using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;
using QuizWebsite.Web;

namespace QuizWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Quiz LoadedQuiz { get; set; } = new Quiz();

        [BindProperty]
        public List<QuizAnswersViewModel> QuizAnswers { get; set; }

        public bool IsSubmitted { get; set; } = false;

        public int CountCorrect { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet() 
        {
            LoadedQuiz = Data.QuizGet.GetQuiz();
            if (TempData.ContainsKey("Posted Score"))
            {
                IsSubmitted = true;
                CountCorrect = (int) TempData["Posted Score"];
            }
        }

        public IActionResult OnPost()
        {
            LoadedQuiz = Data.QuizGet.GetQuiz();
            var scoringResult = QuizScore.GetNumberCorrect(LoadedQuiz, QuizAnswers);
            TempData.Add("Posted Score", scoringResult.CorrectCount);
            return RedirectToPage("./Index");
        }
    }
}