namespace QuizWebsite.Core.Models
{
    public class QuestionResponse
    {
        public long QuizAttemptId { get; set; }

        public long QuestionId { get; set; }

        public bool AnsweredCorrectly { get; set; }
    }
}
