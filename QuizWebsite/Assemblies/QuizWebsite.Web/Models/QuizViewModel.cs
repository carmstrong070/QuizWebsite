using QuizWebsite.Core.Models;
using QuizWebsite.Pages;

namespace QuizWebsite.Web.Models
{
    public class QuizViewModel
    {

        public Quiz LoadedQuiz { get; set; } = new Quiz();

        public List<QuestionResponseViewModel> QuestionResponses { get; set; }

        public bool IsSubmitted { get; set; } = false;

        public int CountCorrect { get; set; }

        public decimal? GlobalAverageScore { get; set; }

    }
}