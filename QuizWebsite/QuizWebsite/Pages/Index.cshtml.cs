using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        //public List<QuizQuestion> QuestionList = new List<QuizQuestion>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
            //QuestionList.Add(new QuizQuestionType()
            //{
            //    QuestionText = "Question 1",
            //    Responses = { { "1", "Answer 1" }, { "2", "Answer 2" }, { "3", "Answer 3" } }
            //});

            //var question2 = new QuizQuestionType();
            //question2.QuestionText = "Question 2";
            //question2.Responses.Add("1", "Answer 1");
            //question2.Responses.Add("2", "Answer 2");
            //question2.Responses.Add("3", "Answer 3");
            //QuestionList.Add(question2);

            //var question3 = new QuizQuestionType("Question 3", new Dictionary<string, string> { { "1", "Answer 1" }, { "2", "Answer 2" }, { "3", "Answer 3" } });
            //QuestionList.Add(question3);

            
            

        }
    }
}
