using QuizWebsite.Data;

namespace QuizWebsite.Web.Test
{
    [TestClass]
    public class TheAuditorAuditor
    {
        [TestMethod]
        public void TestGetAverageQuizScore()
        {
            long userId = 1;
            var result = TheAuditor.GetAverageQuizScore(userId);
            Console.WriteLine(result);
        }
    }
}
