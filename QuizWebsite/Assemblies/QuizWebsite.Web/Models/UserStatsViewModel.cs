using QuizWebsite.Core.Models;

namespace QuizWebsite.Web.Models
{
    public class UserStatsViewModel
    {
        public TimeSpan? TotalTimeQuizzing { get; set; }

        public float? AverageScore { get; set; }

        public Quiz LastQuizCompleted { get; set; }
    }
}
