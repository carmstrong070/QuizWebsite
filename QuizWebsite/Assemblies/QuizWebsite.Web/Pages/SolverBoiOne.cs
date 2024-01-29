using QuizWebsite.Core.Models;
using QuizWebsite.Core.Models.Base;
using QuizWebsite.Pages;

namespace QuizWebsite.Web.Pages
{
    public static class SolverBoiOne
    {
        public static (int CorrectCount, int TotalCount) Solve(Quiz quiz, List<QuizAnswersViewModel> answers)
        {
            var evaluationPairList = new List<(QuizQuestion Question, bool Correct)>();

            foreach (var question in quiz.Questions)
            {
                if (question is SelectQuestion)
                {
                    var selectQuestion = (SelectQuestion)question;
                    var matchingResponse = answers.FirstOrDefault(x => x.QuestionId == selectQuestion.QuestionId);
                    if (matchingResponse != null)
                    {
                        matchingResponse.Answers.RemoveAll(x => x == "false");
                        if (matchingResponse.Answers.SequenceEqual(selectQuestion.AnswerOptions.Where(x => x.IsCorrect).Select(x => x.Id.ToString())))
                        {
                            evaluationPairList.Add((selectQuestion, true));
                            continue;
                        }
                    }
                }
                else if (question is TextQuestion)
                {
                    var textQuestion = (TextQuestion)question;
                    var matchingResponse = answers.FirstOrDefault(x => x.QuestionId == textQuestion.QuestionId);
                    if (matchingResponse != null && matchingResponse.Answers.FirstOrDefault() == textQuestion.AnswerText)
                    {
                        evaluationPairList.Add((textQuestion, true));
                        continue;
                    }
                }
                else
                    throw new NotSupportedException("Question type isn't supported.");

                evaluationPairList.Add((question, false));
            }

            long questionId = 0;

            return (CorrectCount: evaluationPairList.Count(x => x.Correct), TotalCount: evaluationPairList.Count());
        }
    }
}