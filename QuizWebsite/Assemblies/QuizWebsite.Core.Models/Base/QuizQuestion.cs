namespace QuizWebsite.Core.Models.Base
{
    public abstract class QuizQuestion
    {

        public long QuestionId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionTypeName { get; set; }

    }
}