using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using QuizWebsite.Data;
using QuizWebsite.Web.Authentication.Models;
using System.Security.Claims;
using System.Text;

namespace QuizWebsite.Web.Authentication
{
    public class UserManager : IUserManager
    {

        public const string IsAdmininaterRole = "admininater";
        public const string IsPlebRole = "pleb";
        public SignUpResult SignUp(string username, string email, string password, bool isAdmininater = false)
        {

            if (UserHandler.CheckUserExists(username, email))
                return new SignUpResult(error: SignUpResultError.UserAlreadyExists);


            string salt = PasswordHasher.GenerateNewSalt();
            string hashedPassword = PasswordHasher.ComputeHash(password, Encoding.ASCII.GetBytes(salt));

            var userId = UserHandler.CreateUser(username, email, hashedPassword, salt, isAdmininater);

            var user = new UserAuthenticationModel();
            user.Username = username;
            user.Id = userId;
            user.IsAdmininater = isAdmininater;

            return new SignUpResult(user);
        }

        public ValidateResult Validate(string email, string password)
        {
            if (email != null)
            {

                var salt = UserHandler.GetUserSalt(email);

                if (!string.IsNullOrEmpty(password) && salt != null)
                {
                    var hashedPassword = PasswordHasher.ComputeHash(password, Encoding.ASCII.GetBytes(salt));

                    var user = UserHandler.GetUserByCredentials(email, hashedPassword);

                    if (user != null)
                    {
                        if (user.GotBanHammer)
                            return new ValidateResult(success: false, error: ValidateResultError.GotBanned);
                        return new ValidateResult(success: true, user: new UserAuthenticationModel() { Id = user.Id, Username = user.Username, IsAdmininater = user.IsAdmininater });
                    }
                }
            }

            return new ValidateResult(success: false, error: ValidateResultError.InvalidCredentials);
        }

        public async Task SignIn(HttpContext httpContext, UserAuthenticationModel user, bool isPersistent = false)
        {
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
              {
                  AllowRefresh = true,
                  //ExpiresUtc = ??? //-- Don't override cookie options in Program.cs
                  IsPersistent = isPersistent
              }
            );
        }

        public async Task LogOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public long? GetCurrentUserId(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            long currentUserId;

            if (!long.TryParse(claim.Value, out currentUserId))
                return null;

            return currentUserId;
        }

        public UserAuthenticationModel GetCurrentUser(HttpContext httpContext)
        {
            long? currentUserId = this.GetCurrentUserId(httpContext);

            if (!currentUserId.HasValue)
                return null;

            var user = UserHandler.GetUserById(currentUserId.Value);

            if (user == null)
                return null;

            return new UserAuthenticationModel() { Id = user.Id, Username = user.Username, IsAdmininater = user.IsAdmininater };
        }

        private IEnumerable<Claim> GetUserClaims(UserAuthenticationModel user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));


            var userRoleClaims = new List<Claim>();
            if (user.IsAdmininater)
                userRoleClaims.Add(new Claim(ClaimTypes.Role, IsAdmininaterRole));
            else
                userRoleClaims.Add(new Claim(ClaimTypes.Role, IsPlebRole));
            claims.AddRange(userRoleClaims);

            return claims;
        }
    }
}