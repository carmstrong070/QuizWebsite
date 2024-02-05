using QuizWebsite.Web.Authentication.Models;

namespace QuizWebsite.Web.Authentication
{
    //public enum SignUpResultError
    //{
    //    CredentialTypeNotFound
    //}

    //public class SignUpResult
    //{
    //    public User User { get; set; }
    //    public bool Success { get; set; }
    //    public SignUpResultError? Error { get; set; }

    //    public SignUpResult(User user = null, bool success = false, SignUpResultError? error = null)
    //    {
    //        this.User = user;
    //        this.Success = success;
    //        this.Error = error;
    //    }
    //}

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
        //SignUpResult SignUp(string name, string credentialTypeCode, string identifier);
        //SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret);
        //void AddToRole(User user, string roleCode);
        //void AddToRole(User user, Role role);
        //void RemoveFromRole(User user, string roleCode);
        //void RemoveFromRole(User user, Role role);
        //ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret);
        //ValidateResult Validate(string credentialTypeCode, string identifier);
        ValidateResult Validate(string email, string password);
        Task SignIn(HttpContext httpContext, UserAuthenticationModel user, bool isPersistent = false);
        Task SignOut(HttpContext httpContext);
        long? GetCurrentUserId(HttpContext httpContext);
        UserAuthenticationModel GetCurrentUser(HttpContext httpContext);
    }
}