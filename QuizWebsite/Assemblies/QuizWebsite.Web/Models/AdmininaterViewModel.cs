using QuizWebsite.Core.Models;

namespace QuizWebsite.Web.Models
{
    public class AdmininaterViewModel
    {
        public List<User> AllUserInfo { get; set; } = new List<User>();

        public string SearchedUsername { get; set; }

        public PrivilegedUserViewModel SelectedUser { get; set; }

        public class PrivilegedUserViewModel
        {
            public long Id { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            public string Email { get; set; }

            public DateTime CreatedTimestamp { get; set; }

            public bool IsAdmininater { get; set; }

        }

    }
}
