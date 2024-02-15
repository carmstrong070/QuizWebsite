namespace QuizWebsite.Core.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        public bool IsAdmininater { get; set; }
    }
}