using QuizWebsite.Core.Models.Base;

namespace QuizWebsite.Core.Models
{
    public class Quiz
    {

        public long QuizId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        public List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();

    }
}