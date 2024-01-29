using System;
using Bogus;
using QuizWebsite.Core.Models;
using QuizWebsite.Pages;

namespace QuizWebsite.Web.Test
{
    [TestClass]
    public partial class QuizScorerUnitTests
    {
        public Faker FakerInstance { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            FakerInstance = new Faker();
        }

        private delegate (int CorrectCount, int TotalCount) SolvingAlgorithm(Quiz quiz, List<QuizAnswersViewModel> answers);

        [TestMethod]
        public void TestDatAlgo()
        {
            int testsToRun = 500000;
            int questionsInQuiz = 50;
            int targetQuizCorrectnessInGeneratedAnswers = 50;
            SolvingAlgorithm solvingAlgo = QuizScore.GetNumberCorrect; //-- Put your algo here!

            Console.WriteLine($"Test Parameters:");
            Console.WriteLine($"\tTests to run: {testsToRun}");
            Console.WriteLine($"\tQuestions in quiz: {questionsInQuiz}");
            Console.WriteLine($"\tTarget quiz correctness in generated answers: {targetQuizCorrectnessInGeneratedAnswers}%");
            Console.WriteLine($"\tAlgorithm: {solvingAlgo.Method.DeclaringType.Name}.{solvingAlgo.Method.Name}()");
            Console.WriteLine(string.Empty);

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            var quiz = GenerateQuiz(questionsInQuiz);
            var answerSets = new List<List<QuizAnswersViewModel>>();
            while (answerSets.Count < testsToRun)
            {
                int diceRollForCorrect = FakerInstance.Random.Int(0, 100);
                bool shouldBeCorrect = targetQuizCorrectnessInGeneratedAnswers >= diceRollForCorrect;

                if (shouldBeCorrect)
                {
                    answerSets.Add(GenerateAnswers(quiz, 100));
                }
                else
                {
                    answerSets.Add(GenerateAnswers(quiz, FakerInstance.Random.Int(0, 50)));
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Time to generate scenarios: {stopwatch.Elapsed.ToString("mm\\:ss\\.fffff")}");
            stopwatch.Reset();

            stopwatch.Start();

            int countOfCorrectQuizes = 0;
            foreach (var answerSet in answerSets)
            {
                var score = solvingAlgo(quiz, answerSet);
                if (score.CorrectCount == score.TotalCount)
                    countOfCorrectQuizes++;
            }

            stopwatch.Stop();

            Console.WriteLine($"Correct quizzes: {countOfCorrectQuizes.ToString()} / {testsToRun} ({((double)countOfCorrectQuizes / testsToRun * 100).ToString("0.00")}%)");
            Console.WriteLine($"Total solve time: {stopwatch.Elapsed.ToString("mm\\:ss\\.fffff")}");
            var averageTime = new TimeSpan(stopwatch.ElapsedTicks / testsToRun);
            Console.WriteLine($"Average time per quiz solve: {averageTime.ToString("mm\\:ss\\.fffff")}");
        }
    }
}