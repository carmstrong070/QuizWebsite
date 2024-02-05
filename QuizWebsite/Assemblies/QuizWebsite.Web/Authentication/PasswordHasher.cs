using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using QuizWebsite.Web.Authentication.Models;

namespace QuizWebsite.Web.Authentication
{
    public static class PasswordHasher
    {
        /// <remarks>
        /// <see url="https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0"/>
        /// </remarks>
        public static string ComputeHash(string password, byte[] salt = null)
        {
            if (salt == null)
            {
                salt = Encoding.ASCII.GetBytes("1e00f96d-db70-46d1-bc18-901a51c4d8c5");
            }
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        /// <remarks>
        /// <see url="https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0"/>
        /// </remarks>
        public static string GenerateNewSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}