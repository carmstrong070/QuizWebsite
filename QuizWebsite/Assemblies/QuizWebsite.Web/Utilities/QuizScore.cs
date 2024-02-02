using QuizWebsite.Core.Models;
using QuizWebsite.Pages;

namespace QuizWebsite.Web.Utilities
{
    public static class QuizScore
    {
        public static (int CorrectCount, int TotalCount) GetNumberCorrect(Quiz quiz, List<QuestionResponseViewModel> questionResponseList)
        {
            var numberCorrect = 0;
            var answerList = new List<string>();

            for (int i = 0; i < questionResponseList.Count; i++)
            {
                var questionId = questionResponseList[i].QuestionId;

                var quizQuestion = quiz.Questions.FirstOrDefault(x => x.QuestionId == questionId);
                switch (quizQuestion.QuestionTypeName)
                {
                    case "single_select":
                        var responseAnswerId = questionResponseList[i].SingleCheckedResponse;
                        var singleSelectQuestion = (SelectQuestion)quizQuestion;
                        var correctAnswerObj = singleSelectQuestion.AnswerOptions.FirstOrDefault(x => x.IsCorrect == true);
                        if (correctAnswerObj != null && correctAnswerObj.Id.ToString() == responseAnswerId)
                        {
                            numberCorrect++;
                        }
                        break;
                    case "multi_select":
                        var checkedResponses = questionResponseList[i].MultiCheckedResponse.Where(x => x.IsChecked == true);
                        if (checkedResponses != null)
                        {
                            var responseAnswerIds = checkedResponses.Select(x => x.AnswerOptionId.ToString());
                            var multiSelectQuestion = (SelectQuestion)quizQuestion;
                            var correctAnswerIds = multiSelectQuestion.AnswerOptions.Where(x => x.IsCorrect == true).Select(x => x.Id.ToString());
                            if (correctAnswerIds.SequenceEqual(responseAnswerIds))
                            {
                                numberCorrect++;
                            }
                        }
                        break;
                    case "free_response":
                    case "fill_in_blank":
                        var responseText = questionResponseList[i].TextResponse;
                        var textQuestion = (TextQuestion)quizQuestion;
                        var correctAnswerText = textQuestion.AnswerText;
                        if (correctAnswerText == responseText)
                        {
                            numberCorrect++;
                        }
                        break;
                }

            }

            return (CorrectCount: numberCorrect, TotalCount: quiz.Questions.Count());
        }
    }
}
