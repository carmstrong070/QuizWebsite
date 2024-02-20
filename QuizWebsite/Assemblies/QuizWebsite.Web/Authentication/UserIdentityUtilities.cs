using System.Security.Claims;

namespace QuizWebsite.Web.Authentication
{
    public static class UserIdentityUtilities
    {

        public static bool IsAdmininater(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null || claimsPrincipal.Claims == null)
                return false;
            return claimsPrincipal.HasClaim(ClaimTypes.Role, UserManager.IsAdmininaterRole);
        }

    }
}
