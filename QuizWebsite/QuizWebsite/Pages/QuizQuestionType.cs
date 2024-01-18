namespace QuizWebsite.Pages
{
    public class QuizQuestionType : QuizQuestion
    {
        
        public Dictionary<string, string> Responses { get; set; } = new Dictionary<string, string>();

        public QuizQuestionType() { }

        public QuizQuestionType(string questionText, Dictionary<string, string> responses)
        {
            QuestionText = questionText;
            Responses = responses;
        }
    }
}
