using QuizWebsite.Web.Authentication.Models;

namespace QuizWebsite.Web.Authentication
{
    public enum SignUpResultError
    {
        UserAlreadyExists
    }

    public class SignUpResult
    {
        public UserAuthenticationModel User { get; set; }
        public SignUpResultError? Error { get; set; }

        public SignUpResult(UserAuthenticationModel user = null, SignUpResultError? error = null)
        {
            this.User = user;
            this.Error = error;
        }
    }

    public enum ValidateResultError
    {
        InvalidCredentials
    }

    public class ValidateResult
    {
        public UserAuthenticationModel User { get; set; }
        public bool Success { get; set; }
        public ValidateResultError? Error { get; set; }

        public ValidateResult(UserAuthenticationModel user = null, bool success = false, ValidateResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }

    public enum ChangeSecretResultError
    {
        CredentialTypeNotFound,
        CredentialNotFound
    }

    public class ChangeSecretResult
    {
        public bool Success { get; set; }
        public ChangeSecretResultError? Error { get; set; }

        public ChangeSecretResult(bool success = false, ChangeSecretResultError? error = null)
        {
            this.Success = success;
            this.Error = error;
        }
    }

    public interface IUserManager
    {
        SignUpResult SignUp(string username, string email, string password, bool isAdmininater = false);
        //void AddToRole(User user, string roleCode);
        //void AddToRole(User user, Role role);
        //void RemoveFromRole(User user, string roleCode);
        //void RemoveFromRole(User user, Role role);
        //ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret);
        //ValidateResult Validate(string credentialTypeCode, string identifier);
        ValidateResult Validate(string email, string password);
        Task SignIn(HttpContext httpContext, UserAuthenticationModel user, bool isPersistent = false);
        Task LogOut(HttpContext httpContext);
        long? GetCurrentUserId(HttpContext httpContext);
        UserAuthenticationModel GetCurrentUser(HttpContext httpContext);
    }
}