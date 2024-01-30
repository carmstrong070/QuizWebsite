using System;
using Bogus;
using QuizWebsite.Core.Models;
using QuizWebsite.Pages;
using QuizWebsite.Web.Pages;

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
            var solvingAlgos = new List<SolvingAlgorithm>()
            {
                QuizScore.GetNumberCorrect,
                SolverBoiOne.Solve,
                SolverBoiTwo.Solve,
                ChristSolver.ChristAlgo,
                ChristSolver.ChristsAlexAlgo,
                //QuizScore.GetNumberCorrect,
            };

            Console.WriteLine($"Test Parameters:");
            Console.WriteLine($"\tTests to run: {testsToRun}");
            Console.WriteLine($"\tQuestions in quiz: {questionsInQuiz}");
            Console.WriteLine($"\tTarget quiz correctness in generated answers: {targetQuizCorrectnessInGeneratedAnswers}%");

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
            Console.WriteLine($"\tTime to generate scenarios: {stopwatch.Elapsed.ToString("mm\\:ss\\.fffff")}");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (var algo in solvingAlgos)
            {
                stopwatch.Reset();
                stopwatch.Start();

                int countOfCorrectQuizes = RunThaAlgo(algo, quiz, answerSets);

                stopwatch.Stop();

                Console.WriteLine($"Algorithm: {algo.Method.DeclaringType.Name}.{algo.Method.Name}()");
                Console.WriteLine($"\tCorrect quizzes: {countOfCorrectQuizes.ToString()} / {testsToRun} ({((double)countOfCorrectQuizes / testsToRun * 100).ToString("0.00")}%)");
                Console.WriteLine($"\tTotal solve time: {stopwatch.Elapsed.ToString("mm\\:ss\\.fffff")}");
                var averageTime = new TimeSpan(stopwatch.ElapsedTicks / testsToRun);
                Console.WriteLine($"\tAverage time per quiz solve: {averageTime.ToString("mm\\:ss\\.fffff")}");
                Console.WriteLine(string.Empty);
            }            
        }

        private static int RunThaAlgo(SolvingAlgorithm algo, Quiz quiz, List<List<QuizAnswersViewModel>> answerSets)
        {
            int countOfCorrectQuizes = 0;
            foreach (var answerSet in answerSets)
            {
                var score = algo(quiz, answerSet);
                if (score.CorrectCount == score.TotalCount)
                    countOfCorrectQuizes++;
            }
            return countOfCorrectQuizes;
        }
    }
}