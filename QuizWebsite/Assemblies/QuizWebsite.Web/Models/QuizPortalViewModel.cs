using QuizWebsite.Core.Models;

namespace QuizWebsite.Web.Models
{
    public class QuizPortalViewModel
    {
        public List<Quiz> Quizzes { get; set; }

        public Dictionary<long, TimeSpan?> AverageCompletionTimes { get; set; }
    }
}
