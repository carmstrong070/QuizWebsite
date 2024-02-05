namespace QuizWebsite.Web.Authentication.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual UserAuthenticationModel User { get; set; }
        public virtual Role Role { get; set; }
    }
}