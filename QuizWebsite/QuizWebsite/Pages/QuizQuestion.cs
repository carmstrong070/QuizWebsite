namespace QuizWebsite.Pages
{
    public class QuizQuestion
    {
        public string QuestionId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionTypeName { get; set; }

        public List<AnswerOption> AnswerOptions { get; set; }

        public string AnswerText { get; set; }

    }

}
