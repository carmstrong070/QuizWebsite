using QuizWebsite.Core.Models;

namespace QuizWebsite.Web.Models
{
    public class AdmininaterViewModel
    {
        public List<User> AllUserInfo { get; set; } = new List<User>();

        public string SearchedUsername { get; set; }

    }
}
