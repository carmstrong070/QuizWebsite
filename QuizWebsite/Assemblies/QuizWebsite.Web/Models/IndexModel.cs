using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebsite.Core.Models;
using QuizWebsite.Pages;
using QuizWebsite.Web;

namespace QuizWebsite.Web.Models
{
    public class IndexModel
    {

        public Quiz LoadedQuiz { get; set; } = new Quiz();

        public List<QuizAnswersViewModel> QuizAnswers { get; set; }

        public bool IsSubmitted { get; set; } = false;

        public int CountCorrect { get; set; }

    }
}