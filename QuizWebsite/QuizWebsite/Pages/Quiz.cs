namespace QuizWebsite.Pages
{
    public class Quiz
    {

        public int QuizId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public List<QuizQuestion> Questions { get; set; }

    }
}
