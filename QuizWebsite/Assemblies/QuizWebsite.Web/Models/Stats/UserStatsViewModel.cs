using QuizWebsite.Core.Models;

namespace QuizWebsite.Web.Models.Stats
{
    public class UserStatsViewModel
    {
        public TimeSpan? TotalTimeQuizzing { get; set; }

        public decimal? AverageQuizScore { get; set; }

        public decimal? OverallQuestionsCorrect { get; set; }

        public Quiz LastQuizCompleted { get; set; }
    }
}
