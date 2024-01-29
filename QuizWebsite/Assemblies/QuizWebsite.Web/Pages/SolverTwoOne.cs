using System.Collections.Concurrent;
using QuizWebsite.Core.Models;
using QuizWebsite.Core.Models.Base;
using QuizWebsite.Pages;

namespace QuizWebsite.Web.Pages
{
    public static class SolverBoiTwo
    {
        public static (int CorrectCount, int TotalCount) Solve(Quiz quiz, List<QuizAnswersViewModel> answers)
        {
            var evaluationPairList = new ConcurrentBag<(QuizQuestion Question, bool Correct)>();
            var tasks = new List<Task>();

            foreach (var question in quiz.Questions)
            {
                tasks.Add(Task.Run(() => {
                    evaluationPairList.Add((question, EvaluateQuestion(question, answers)));
                }));
            }

            var task = Task.WhenAll(tasks);
            task.Wait();

            return (CorrectCount: evaluationPairList.Count(x => x.Correct), TotalCount: evaluationPairList.Count());
        }

        private static bool EvaluateQuestion(QuizQuestion question, List<QuizAnswersViewModel> answers)
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
                        return true;
                    }
                }
            }
            else if (question is TextQuestion)
            {
                var textQuestion = (TextQuestion)question;
                var matchingResponse = answers.FirstOrDefault(x => x.QuestionId == textQuestion.QuestionId);
                if (matchingResponse != null && matchingResponse.Answers.FirstOrDefault() == textQuestion.AnswerText)
                {
                    return true;
                }
            }
            else
                throw new NotSupportedException("Question type isn't supported.");

            return false;
        }
    }
}