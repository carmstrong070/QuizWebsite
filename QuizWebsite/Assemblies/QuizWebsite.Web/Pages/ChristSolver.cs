using QuizWebsite.Core.Models;
using QuizWebsite.Core.Models.Base;
using QuizWebsite.Pages;

namespace QuizWebsite.Web.Pages
{
    public static class ChristSolver
    {
        public static (int CorrectCount, int TotalCount) ChristAlgo(Quiz quiz, List<QuizAnswersViewModel> answers)
        {
            (int CorrectCount, int TotalCount) ret = (0, quiz.Questions.Count);

            foreach (var q in quiz.Questions)
            {
                var answer = answers.FirstOrDefault(x => x.QuestionId == q.QuestionId);
                switch (q.QuestionTypeName)
                {
                    case "fill_in_the_blank":
                    case "free_response":
                        var textQ = q as TextQuestion;
                        if (answer != null && answer.Answers.Any(y => y == textQ.AnswerText))
                            ret.CorrectCount++;
                        break;
                    case "single_select":
                        var singleQ = q as SelectQuestion;
                        if (answer != null && answer.Answers.Any(x => singleQ.AnswerOptions.Where(x => x.IsCorrect).Select(y => y.Id.ToString()).Contains(x)))
                            ret.CorrectCount++;
                        break;
                    case "multi_select":
                        var multiQ = q as SelectQuestion;
                        answer.Answers.RemoveAll(x => x == "false");
                        if (answer != null && answer.Answers.SequenceEqual(multiQ.AnswerOptions.Where(x => x.IsCorrect).Select(y => y.Id.ToString()).ToList()))
                            ret.CorrectCount++;
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }

        public static (int CorrectCount, int TotalCount) ChristsAlexAlgo(Quiz quiz, List<QuizAnswersViewModel> answers)
        {
            (int CorrectCount, int TotalCount) ret = (0, quiz.Questions.Count);

            foreach (var a in answers)
            {
                var question = quiz.Questions.FirstOrDefault(x => x.QuestionId == a.QuestionId);
                switch (question.QuestionTypeName)
                {
                    case "fill_in_the_blank":
                    case "free_response":
                        var textQ = question as TextQuestion;
                        if (a.Answers.Any(y => y == textQ.AnswerText))
                            ret.CorrectCount++;
                        break;
                    case "single_select":
                        var singleQ = question as SelectQuestion;
                        if (a.Answers.Any(x => singleQ.AnswerOptions.Where(x => x.IsCorrect).Select(y => y.Id.ToString()).Contains(x)))
                            ret.CorrectCount++;
                        break;
                    case "multi_select":
                        var multiQ = question as SelectQuestion;
                        a.Answers.RemoveAll(x => x == "false");
                        if (a.Answers.SequenceEqual(multiQ.AnswerOptions.Where(x => x.IsCorrect).Select(y => y.Id.ToString()).ToList()))
                            ret.CorrectCount++;
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }
    }
}