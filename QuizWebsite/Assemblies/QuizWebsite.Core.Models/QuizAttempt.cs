namespace QuizWebsite.Core.Models
{
    public class QuizAttempt
    {
        public long Id { get; set; }

        public long QuizId { get; set; }

        public long? UserId { get; set; }

        public DateTime start_timestamp { get; set; }

        public DateTime end_timestamp { get; set; }
    }
}
