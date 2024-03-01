using QuizWebsite.Core.Models;

namespace QuizWebsite.Web.Models.QuizGame
{
    public class QuizViewModel
    {

        public Quiz LoadedQuiz { get; set; } = new Quiz();

        public List<QuestionResponseViewModel> QuestionResponses { get; set; }

        public bool IsSubmitted { get; set; } = false;

        public int CountCorrect { get; set; }

        public decimal? GlobalAverageScore { get; set; }

        public Dictionary<long, decimal> GlobalAverageQuestionScore { get; set; }

    }
}