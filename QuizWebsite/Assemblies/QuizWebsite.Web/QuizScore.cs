using QuizWebsite.Core.Models;
using QuizWebsite.Pages;

namespace QuizWebsite.Web
{
    public static class QuizScore
    {
        public static (int CorrectCount, int TotalCount) GetNumberCorrect(Quiz quiz, List<QuizAnswersViewModel> questionResponseList)
        {
            var numberCorrect = 0;

            for (int i = 0; i < questionResponseList.Count; i++)
            {
                var questionId = questionResponseList[i].QuestionId;
                var answerList = questionResponseList[i].Answers;

                    var quizQuestion = quiz.Questions.FirstOrDefault(x => x.QuestionId == questionId);
                    switch(quizQuestion.QuestionTypeName)
                    {
                        case "single_select":
                        case "multi_select":
                            var multiSelectQuestion = (SelectQuestion)quizQuestion;
                            answerList.RemoveAll(x => x == "false");
                            var correctAnswerIds = multiSelectQuestion.AnswerOptions.Where(x => x.IsCorrect == true).Select(x => x.Id.ToString());
                            if (correctAnswerIds.SequenceEqual(answerList))
                            {
                                numberCorrect++;
                            }
                            break;
                        case "free_response":
                        case "fill_in_blank":
                            var textQuestion = (TextQuestion)quizQuestion;
                            var correctAnswerText = textQuestion.AnswerText;
                            if (answerList.Count() > 0 && correctAnswerText == answerList[0])
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
