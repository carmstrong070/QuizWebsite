using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebsite.Core.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}