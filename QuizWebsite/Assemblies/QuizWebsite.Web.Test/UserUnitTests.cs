using QuizWebsite.Data;
using QuizWebsite.Web.Authentication;

namespace QuizWebsite.Web.Test
{
    [TestClass]
    public class UserUnitTests
    {
        [TestMethod]
        public void SetUserPassword()
        {
            string hashedPassword = PasswordHasher.ComputeHash("123456");

            UserGrabber.UpdatePassword(1, hashedPassword);
        }
    }
}