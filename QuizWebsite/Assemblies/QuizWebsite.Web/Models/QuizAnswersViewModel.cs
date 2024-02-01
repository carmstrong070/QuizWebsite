namespace QuizWebsite.Pages
{
    public class QuizAnswersViewModel
    {
        public long QuestionId { get; set; }

        public List<string> Answers { get; set; } = new List<string>();

        //TODO: Optimize this jank
        public List<CheckboxOptions> CheckedAnswers { get; set; }

        public class CheckboxOptions
        {
            public int AnswerOptionId { get; set; }

            public bool Checked { get; set; }
        }
    }
}
