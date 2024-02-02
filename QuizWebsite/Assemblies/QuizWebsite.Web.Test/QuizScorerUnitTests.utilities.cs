using Bogus;
using QuizWebsite.Core.Models;
using QuizWebsite.Core.Models.Base;
using QuizWebsite.Pages;

namespace QuizWebsite.Web.Test
{
    public partial class QuizScorerUnitTests
    {
        private const string GeneratedAuthorName = "QuizScorerFidelityUnitTests";
        private const string SingleSelectQuestionTypeName = "single_select";
        private const string MultiSelectQuestionTypeName = "multi_select";
        private const string FreeResponseQuestionTypeName = "free_response";
        private const string FillInBlankQuestionTypeName = "fill_in_blank";

        private Quiz GenerateQuiz(int quizQuestions = 100)
        {
            var quiz = new Quiz()
            {
                Title = "GenerateQuiz",
                Author = GeneratedAuthorName,
                CreatedTimestamp = DateTime.Now,
            };

            int currentQuestionId = 1;
            string[] questionTypes = { SingleSelectQuestionTypeName, MultiSelectQuestionTypeName, FreeResponseQuestionTypeName };

            while (quiz.Questions.Count() < quizQuestions)
            {
                string selectedQuestionType = FakerInstance.PickRandom(questionTypes);
                switch (selectedQuestionType)
                {
                    case SingleSelectQuestionTypeName:
                        quiz.Questions.Add(GenerateRadioButtonQuestion(currentQuestionId));
                        break;
                    case MultiSelectQuestionTypeName:
                        quiz.Questions.Add(GenerateCheckboxQuestion(currentQuestionId));
                        break;
                    case FreeResponseQuestionTypeName:
                        quiz.Questions.Add(GenerateTextQuestion(currentQuestionId));
                        break;
                }

                currentQuestionId++;
            }

            return quiz;
        }

        private Quiz GenerateRadioButtonOnlyQuiz()
        {
            var quiz = new Quiz()
            {
                Title = "GenerateRadioButtonOnlyQuiz",
                Author = GeneratedAuthorName,
                CreatedTimestamp = DateTime.Now,
            };

            int currentQuestionId = 1;

            while (quiz.Questions.Count() < 10)
            {
                quiz.Questions.Add(GenerateRadioButtonQuestion(currentQuestionId));

                currentQuestionId++;
            }

            return quiz;
        }

        private Quiz GenerateCheckboxOnlyQuiz()
        {
            var quiz = new Quiz()
            {
                Title = "GenerateCheckboxOnlyQuiz",
                Author = GeneratedAuthorName,
                CreatedTimestamp = DateTime.Now,
            };

            int currentQuestionId = 1;

            while (quiz.Questions.Count() < 10)
            {
                quiz.Questions.Add(GenerateCheckboxQuestion(currentQuestionId));

                currentQuestionId++;
            }

            return quiz;
        }

        private Quiz GenerateTextboxOnlyQuiz()
        {
            var quiz = new Quiz()
            {
                Title = "GenerateTextboxOnlyQuiz",
                Author = GeneratedAuthorName,
                CreatedTimestamp = DateTime.Now,
            };

            long currentQuestionId = 1;

            while (quiz.Questions.Count() < 10)
            {
                quiz.Questions.Add(GenerateTextQuestion(currentQuestionId));

                currentQuestionId++;
            }

            return quiz;
        }

        private SelectQuestion GenerateRadioButtonQuestion(long currentQuestionId)
        {
            var selectQuestion = new SelectQuestion()
            {
                QuestionId = currentQuestionId,
                QuestionText = $"Question {currentQuestionId}. What is the answer?",
                QuestionTypeName = SingleSelectQuestionTypeName
            };

            int currentAnswerOptionId = 1;
            int maxOptions = FakerInstance.Random.Int(2, 4);
            int correctOptionId = FakerInstance.Random.Int(currentAnswerOptionId, maxOptions);

            while (selectQuestion.AnswerOptions.Count() < maxOptions)
            {
                var answerOption = new AnswerOption()
                {
                    Id = currentAnswerOptionId,
                    IsCorrect = correctOptionId == currentAnswerOptionId,
                    OptionText = FakerInstance.Random.Word()
                };
                selectQuestion.AnswerOptions.Add(answerOption);

                currentAnswerOptionId++;
            }

            return selectQuestion;
        }

        private SelectQuestion GenerateCheckboxQuestion(long currentQuestionId)
        {
            var selectQuestion = new SelectQuestion()
            {
                QuestionId = currentQuestionId,
                QuestionText = $"Question {currentQuestionId}. What is the answer?",
                QuestionTypeName = MultiSelectQuestionTypeName
            };

            int currentAnswerOptionId = 1;
            int maxOptions = FakerInstance.Random.Int(5, 10);

            while (selectQuestion.AnswerOptions.Count() < maxOptions)
            {
                var answerOption = new AnswerOption()
                {
                    Id = currentAnswerOptionId,
                    IsCorrect = FakerInstance.Random.Bool(),
                    OptionText = FakerInstance.Random.Word()
                };
                selectQuestion.AnswerOptions.Add(answerOption);

                currentAnswerOptionId++;
            }

            return selectQuestion;
        }

        private TextQuestion GenerateTextQuestion(long currentQuestionId)
        {
            return new TextQuestion()
            {
                QuestionId = currentQuestionId,
                QuestionText = $"Question {currentQuestionId}. What is the answer?",
                QuestionTypeName = FreeResponseQuestionTypeName,
                AnswerText = FakerInstance.Random.Words(FakerInstance.Random.Int(1, 5))
            };
        }

        private List<QuestionResponseViewModel> GenerateAnswers(Quiz quiz, int percentageCorrect)
        {
            var answers = new List<QuestionResponseViewModel>();

            foreach (var question in quiz.Questions)
            {
                var response = new QuestionResponseViewModel()
                {
                    QuestionId = question.QuestionId
                };

                int diceRollForCorrect = FakerInstance.Random.Int(0, 100);
                bool shouldBeCorrect = percentageCorrect >= diceRollForCorrect;

                switch (question.QuestionTypeName)
                {
                    case SingleSelectQuestionTypeName:
                        response.TextResponse = GenerateRadioButtonAnswers((SelectQuestion)question, shouldBeCorrect);
                        break;
                    case MultiSelectQuestionTypeName:
                        response.TextResponse = GenerateCheckboxAnswers((SelectQuestion)question, shouldBeCorrect);
                        break;
                    case FreeResponseQuestionTypeName:
                        response.TextResponse = GenerateTextAnswer((TextQuestion)question, shouldBeCorrect);
                        break;
                }

                answers.Add(response);
            }

            return answers;
        }

        private List<string> GenerateRadioButtonAnswers(SelectQuestion question, bool isCorrect)
        {
            var answers = new List<string>();

            if (isCorrect)
            {
                var correctAnswerOption = question.AnswerOptions.FirstOrDefault(x => x.IsCorrect);
                answers.Add(correctAnswerOption.Id.ToString());
            }
            else
            {
                if (FakerInstance.Random.Bool()) //-- Chance to not have any answer
                {
                    var correctAnswerOption = question.AnswerOptions.FirstOrDefault(x => x.IsCorrect);

                    string wrongAnswer;
                    do
                    {
                        wrongAnswer = FakerInstance.Random.Int((int)question.AnswerOptions.Min(x => x.Id), (int)question.AnswerOptions.Max(x => x.Id)).ToString();
                    }
                    while (wrongAnswer == correctAnswerOption.Id.ToString());
                    answers.Add(wrongAnswer);
                }
            }

            return answers;
        }

        private List<string> GenerateCheckboxAnswers(SelectQuestion question, bool isCorrect)
        {
            var answers = new List<string>();

            if (isCorrect)
            {
                foreach (var answerOption in question.AnswerOptions)
                {
                    if (answerOption.IsCorrect)
                        answers.Add(answerOption.Id.ToString());
                    else
                        answers.Add("false");
                }
            }
            else
            {
                foreach (var answerOption in question.AnswerOptions)
                {
                    if (FakerInstance.Random.Bool()) //-- Chance to not have any answer
                    {
                        string wrongAnswer;
                        do
                        {
                            if (FakerInstance.Random.Bool())
                            {
                                wrongAnswer = FakerInstance.Random.Int((int)question.AnswerOptions.Min(x => x.Id), (int)question.AnswerOptions.Max(x => x.Id)).ToString();
                            }
                            else
                                wrongAnswer = "false";
                        }
                        while (question.AnswerOptions.Where(x => x.IsCorrect).Select(x => x.Id.ToString()).Contains(wrongAnswer));
                        answers.Add(wrongAnswer);
                    }
                }
            }

            return answers;
        }

        private List<string> GenerateTextAnswer(TextQuestion question, bool isCorrect)
        {
            var answers = new List<string>();

            if (isCorrect)
            {
                answers.Add(question.AnswerText);
            }
            else
            {
                if (FakerInstance.Random.Bool()) //-- Chance to not have any answer
                {
                    string wrongAnswer;
                    do
                    {
                        wrongAnswer = FakerInstance.Random.Word();
                    }
                    while (wrongAnswer == question.AnswerText);
                    answers.Add(wrongAnswer);
                }
            }

            return answers;
        }
    }
}