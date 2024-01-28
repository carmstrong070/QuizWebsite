namespace QuizWebsite.Core.Models
{
    public class AnswerOption
    {
        public long Id { get; set; }

        public string OptionText { get; set; }

        public bool IsCorrect { get; set; }

    }
}