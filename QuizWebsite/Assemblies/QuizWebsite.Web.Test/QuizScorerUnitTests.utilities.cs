using QuizWebsite.Core.Models;
using QuizWebsite.Web.Models.Quiz;
using static QuizWebsite.Web.Models.Quiz.QuestionResponseViewModel;

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
                        response.SingleCheckedResponse = GenerateRadioButtonAnswers((SelectQuestion)question, shouldBeCorrect);
                        break;
                    case MultiSelectQuestionTypeName:
                        response.MultiCheckedResponse = GenerateCheckboxAnswers((SelectQuestion)question, shouldBeCorrect);
                        break;
                    case FreeResponseQuestionTypeName:
                        response.TextResponse = GenerateTextAnswer((TextQuestion)question, shouldBeCorrect);
                        break;
                }

                answers.Add(response);
            }

            return answers;
        }

        private string GenerateRadioButtonAnswers(SelectQuestion question, bool isCorrect)
        {
            string answer = null;

            if (isCorrect)
            {
                var correctAnswerOption = question.AnswerOptions.FirstOrDefault(x => x.IsCorrect);
                answer = correctAnswerOption.Id.ToString();
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
                    answer = wrongAnswer;
                }
            }

            return answer;
        }

        private List<CheckboxOptions> GenerateCheckboxAnswers(SelectQuestion question, bool isCorrect)
        {
            var answers = new List<CheckboxOptions>();

            //-- Always populate with correct answers
            //-- Incorrect-fill will jack up the correct answers
            foreach (var answerOption in question.AnswerOptions)
            {
                if (answerOption.IsCorrect)
                    answers.Add(new CheckboxOptions() { AnswerOptionId = answerOption.Id, IsChecked = true });
                else
                    answers.Add(new CheckboxOptions() { AnswerOptionId = answerOption.Id, IsChecked = false });
            }

            if (!isCorrect)
            {
                do
                {
                    foreach (var answerResponse in answers)
                    {
                        if (FakerInstance.Random.Bool())
                            answerResponse.IsChecked = !answerResponse.IsChecked; //-- Rando flip!
                    }
                }
                while (question.AnswerOptions.Where(x => x.IsCorrect).Select(x => x.Id).SequenceEqual(answers.Select(x => x.AnswerOptionId))); //-- Dope
            }

            return answers;
        }

        private string GenerateTextAnswer(TextQuestion question, bool isCorrect)
        {
            string answer = null;

            if (isCorrect)
            {
                answer = question.AnswerText;
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
                    answer = wrongAnswer;
                }
            }

            return answer;
        }
    }
}