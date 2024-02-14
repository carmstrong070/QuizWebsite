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
            var result = TheAuditor.GetAverageUserQuizScore(userId);
            Console.WriteLine(result);
        }
    }
}
