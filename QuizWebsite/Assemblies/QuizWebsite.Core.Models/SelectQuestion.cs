using QuizWebsite.Core.Models.Base;

namespace QuizWebsite.Core.Models
{
    public class SelectQuestion : QuizQuestion
    {
        public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();

    }
}