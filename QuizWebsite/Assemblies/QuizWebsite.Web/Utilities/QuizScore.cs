using QuizWebsite.Core.Models;
using QuizWebsite.Web.Models.QuizGame;

namespace QuizWebsite.Web.Utilities
{
    public static class QuizScore
    {
        public static Dictionary<long, bool> GetNumberCorrect(Quiz quiz, List<QuestionResponseViewModel> questionResponseList)
        {
            var scoringResultDict = new Dictionary<long, bool>();

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
                            scoringResultDict.Add(questionId, true);
                        else
                            scoringResultDict.Add(questionId, false);
                        break;
                    case "multi_select":
                        var checkedResponses = questionResponseList[i].MultiCheckedResponse.Where(x => x.IsChecked == true);
                        if (checkedResponses != null)
                        {
                            var responseAnswerIds = checkedResponses.Select(x => x.AnswerOptionId.ToString());
                            var multiSelectQuestion = (SelectQuestion)quizQuestion;
                            var correctAnswerIds = multiSelectQuestion.AnswerOptions.Where(x => x.IsCorrect == true).Select(x => x.Id.ToString());
                            if (correctAnswerIds.SequenceEqual(responseAnswerIds))
                                scoringResultDict.Add(questionId, true);
                            else
                                scoringResultDict.Add(questionId, false);

                        }
                        break;
                    case "free_response":
                    case "fill_in_blank":
                        var responseText = questionResponseList[i].TextResponse;
                        var textQuestion = (TextQuestion)quizQuestion;
                        var correctAnswerText = textQuestion.AnswerText;
                        if (correctAnswerText == responseText)
                            scoringResultDict.Add(questionId, true);
                        else
                            scoringResultDict.Add(questionId, false);
                        break;
                }

            }

            return scoringResultDict;
        }
    }
}
