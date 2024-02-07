using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;
using System.Security.Claims;

namespace QuizWebsite.Web.Controllers
{
    [Authorize]
    public class AuthenticatedControllerBase : Controller
    {
        protected IUserManager AuthUserManager { get; set; }

        protected long? UserId
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    return long.Parse(this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                }
                return null;
            }
        }

        public AuthenticatedControllerBase(IUserManager authUserManager)
        {
            AuthUserManager = authUserManager;
        }
    }
}
