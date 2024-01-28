namespace QuizWebsite.Pages
{
    public class QuizAnswersViewModel
    {
        public long QuestionId { get; set; }

        public List<string> Answers { get; set; } = new List<string>();
    }
}
