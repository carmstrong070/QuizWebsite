using QuizWebsite.Data;
using QuizWebsite.Web.Authentication;
using System.Text;

namespace QuizWebsite.Web.Test
{
    [TestClass]
    public class UserUnitTests
    {
        [TestMethod]
        public void SetUserPassword()
        {
            var salt = PasswordHasher.GenerateNewSalt();
            string hashedPassword = PasswordHasher.ComputeHash("123456", Encoding.ASCII.GetBytes(salt));

            UserGrabber.UpdatePassword(1, hashedPassword, salt);

        }
    }
}