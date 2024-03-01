using QuizWebsite.Core.Models;

namespace QuizWebsite.Web.Models.QuizGame
{
    public class QuizPortalViewModel
    {
        public List<Quiz> Quizzes { get; set; }

        public Dictionary<long, TimeSpan?> AverageCompletionTimes { get; set; }

        public Dictionary<long, int> TotalAttempts { get; set; }
    }
}
