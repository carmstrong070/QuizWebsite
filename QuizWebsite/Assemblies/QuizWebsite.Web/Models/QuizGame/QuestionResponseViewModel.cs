namespace QuizWebsite.Web.Models.QuizGame
{
    public class QuestionResponseViewModel
    {
        public long QuestionId { get; set; }

        public string TextResponse { get; set; }

        public string SingleCheckedResponse { get; set; }

        public List<CheckboxOptions> MultiCheckedResponse { get; set; }

        public class CheckboxOptions
        {
            public long AnswerOptionId { get; set; }

            public bool IsChecked { get; set; }
        }
    }
}
