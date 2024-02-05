namespace QuizWebsite.Web.Authentication.Models
{
    public class UserAuthenticationModel
    {
        public long Id { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Credential> Credentials { get; set; }
    }
}