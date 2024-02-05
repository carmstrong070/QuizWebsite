using Bogus;
using QuizWebsite.Core.Models;
using QuizWebsite.Data;
using QuizWebsite.Pages;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Utilities;

namespace QuizWebsite.Web.Test
{
    [TestClass]
    public class UserUnitTests
    {
        [TestMethod]
        public void SetUserPassword()
        {
            string hashedPassword = PasswordHasher.ComputeHash("Navy_2005Navy_2005");

            UserGrabber.UpdatePassword(1, hashedPassword);
        }
    }
}