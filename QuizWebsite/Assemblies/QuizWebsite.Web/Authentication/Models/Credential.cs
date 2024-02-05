namespace QuizWebsite.Web.Authentication.Models
{
    public class Credential
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CredentialTypeId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PassworldSalt { get; set; }

        public virtual UserAuthenticationModel User { get; set; }
        public virtual CredentialType CredentialType { get; set; }
    }
}