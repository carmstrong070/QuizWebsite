﻿namespace QuizWebsite.Web.Authentication.Models
{
    public class CredentialType
    {
        public int Id { get; set; }
        //public string Code { get; set; }
        public string Name { get; set; }
        //public int? Position { get; set; }

        public virtual ICollection<Credential> Credentials { get; set; }
    }
}