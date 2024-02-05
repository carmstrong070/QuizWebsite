using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using QuizWebsite.Data;
using QuizWebsite.Web.Authentication.Models;

namespace QuizWebsite.Web.Authentication
{
    public class UserManager : IUserManager
    {
        //public SignUpResult SignUp(string name, string credentialTypeCode, string identifier)
        //{
        //    return this.SignUp(name, credentialTypeCode, identifier, null);
        //}

        //public SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret)
        //{
        //    User user = new User();

        //    user.Name = name;
        //    user.Created = DateTime.Now;
        //    this.storage.Users.Add(user);
        //    this.storage.SaveChanges();

        //    CredentialType credentialType = this.storage.CredentialTypes.FirstOrDefault(ct => ct.Code.ToLower() == credentialTypeCode.ToLower());

        //    if (credentialType == null)
        //        return new SignUpResult(success: false, error: SignUpResultError.CredentialTypeNotFound);

        //    Credential credential = new Credential();

        //    credential.UserId = user.Id;
        //    credential.CredentialTypeId = credentialType.Id;
        //    credential.Identifier = identifier;

        //    if (!string.IsNullOrEmpty(secret))
        //    {
        //        byte[] salt = PasswordHasher.GenerateRandomSalt();
        //        string hash = PasswordHasher.ComputeHash(secret, salt);

        //        credential.Secret = hash;
        //        credential.Extra = Convert.ToBase64String(salt);
        //    }

        //    this.storage.Credentials.Add(credential);
        //    this.storage.SaveChanges();
        //    return new SignUpResult(user: user, success: true);
        //}

        //public void AddToRole(User user, string roleCode)
        //{
        //    Role role = this.storage.Roles.FirstOrDefault(r => r.Code.ToLower() == roleCode.ToLower());

        //    if (role == null)
        //        return;

        //    this.AddToRole(user, role);
        //}

        //public void AddToRole(User user, Role role)
        //{
        //    UserRole userRole = this.storage.UserRoles.Find(user.Id, role.Id);

        //    if (userRole != null)
        //        return;

        //    userRole = new UserRole();
        //    userRole.UserId = user.Id;
        //    userRole.RoleId = role.Id;
        //    this.storage.UserRoles.Add(userRole);
        //    this.storage.SaveChanges();
        //}

        //public void RemoveFromRole(User user, string roleCode)
        //{
        //    Role role = this.storage.Roles.FirstOrDefault(r => r.Code.ToLower() == roleCode.ToLower());

        //    if (role == null)
        //        return;

        //    this.RemoveFromRole(user, role);
        //}

        //public void RemoveFromRole(User user, Role role)
        //{
        //    UserRole userRole = this.storage.UserRoles.Find(user.Id, role.Id);

        //    if (userRole == null)
        //        return;

        //    this.storage.UserRoles.Remove(userRole);
        //    this.storage.SaveChanges();
        //}

        //public ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret)
        //{
        //    CredentialType credentialType = this.storage.CredentialTypes.FirstOrDefault(ct => ct.Code.ToLower() == credentialTypeCode.ToLower());

        //    if (credentialType == null)
        //        return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialTypeNotFound);

        //    Credential credential = this.storage.Credentials.FirstOrDefault(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier);

        //    if (credential == null)
        //        return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialNotFound);

        //    byte[] salt = PasswordHasher.GenerateRandomSalt();
        //    string hash = PasswordHasher.ComputeHash(secret, salt);

        //    credential.PasswordHash = hash;
        //    credential.PasswordSalt = Convert.ToBase64String(salt);
        //    this.storage.Credentials.Update(credential);
        //    this.storage.SaveChanges();
        //    return new ChangeSecretResult(success: true);
        //}

        //public ValidateResult Validate(string credentialTypeCode, string identifier)
        //{
        //    return this.Validate(credentialTypeCode, identifier, null);
        //}

        public ValidateResult Validate(string email, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                string hashedPassword = PasswordHasher.ComputeHash(password);

                var user = UserGrabber.GetUserByCredentials(email, hashedPassword);

                if (user != null)
                    return new ValidateResult(success: true, user: new UserAuthenticationModel() { Id = user.Id, Username = user.Username });
            }

            return new ValidateResult(success: false, error: ValidateResultError.InvalidCredentials);
        }

        public async Task SignIn(HttpContext httpContext, UserAuthenticationModel user, bool isPersistent = false)
        {
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
              {
                  AllowRefresh = true,
                  //ExpiresUtc = ??? //-- Don't override cookie options in Program.cs
                  IsPersistent = isPersistent
              }
            );
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public long? GetCurrentUserId(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            long currentUserId;

            if (!long.TryParse(claim.Value, out currentUserId))
                return null;

            return currentUserId;
        }

        public UserAuthenticationModel GetCurrentUser(HttpContext httpContext)
        {
            long? currentUserId = this.GetCurrentUserId(httpContext);

            if (!currentUserId.HasValue)
                return null;

            var user = UserGrabber.GetUserById(currentUserId.Value);

            if (user == null)
                return null;

            return new UserAuthenticationModel() { Id = user.Id, Username = user.Username };
        }

        private IEnumerable<Claim> GetUserClaims(UserAuthenticationModel user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

            //TODO: Make an admin role
            var userRoleClaims = new List<Claim>();
            userRoleClaims.Add(new Claim(ClaimTypes.Role, "NormalGuy"));
            claims.AddRange(userRoleClaims);

            return claims;
        }

        //private IEnumerable<Claim> GetUserRoleClaims(UserAuthenticationModel user)
        //{
        //    List<Claim> claims = new List<Claim>();
        //    IEnumerable<int> roleIds = this.storage.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId).ToList();

        //    if (roleIds != null)
        //    {
        //        foreach (int roleId in roleIds)
        //        {
        //            Role role = this.storage.Roles.Find(roleId);

        //            claims.Add(new Claim(ClaimTypes.Role, role.Code));
        //            claims.AddRange(this.GetUserPermissionClaims(role));
        //        }
        //    }

        //    return claims;
        //}

        //private IEnumerable<Claim> GetUserPermissionClaims(Role role)
        //{
        //    List<Claim> claims = new List<Claim>();
        //    IEnumerable<int> permissionIds = this.storage.RolePermissions.Where(rp => rp.RoleId == role.Id).Select(rp => rp.PermissionId).ToList();

        //    if (permissionIds != null)
        //    {
        //        foreach (int permissionId in permissionIds)
        //        {
        //            Permission permission = this.storage.Permissions.Find(permissionId);

        //            claims.Add(new Claim("Permission", permission.Code));
        //        }
        //    }

        //    return claims;
        //}
    }
}