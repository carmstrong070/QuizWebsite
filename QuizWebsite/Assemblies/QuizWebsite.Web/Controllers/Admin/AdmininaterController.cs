using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models.Admin;
using System.Text;

namespace QuizWebsite.Web.Controllers.Admin
{
    [Authorize(Roles = UserManager.IsAdmininaterRole)]
    [Area("Admin")]
    public class AdmininaterController : AuthenticatedControllerBase
    {
        public AdmininaterController(IUserManager authUserManager) : base(authUserManager) { }

        [HttpGet]
        [Route("AdmininaterPortal")]
        public IActionResult AdmininaterPortal()
        {
            var vm = new AdmininaterViewModel();
            vm.AllUserInfo = AdmininaterTools.GetAllUsers();
            return View(vm);
        }

        [HttpPost]
        [Route("AdmininaterPortal")]
        public IActionResult AdmininaterPortal(AdmininaterViewModel vm)
        {
            vm.AllUserInfo = AdmininaterTools.GetAllUsers(vm.SearchedUsername);
            return View(vm);
        }

        [HttpPost]
        [Route("UpdateTable")]
        public IActionResult UpdateTable(string searchedUser)
        {
            var userList = AdmininaterTools.GetAllUsers(searchedUser);
            return PartialView("_AdmininaterTable", userList);
        }

        [HttpPost]
        [Route("SelectUser")]
        public IActionResult SelectUser(long id)
        {
            var privilegedUserViewModel = new AdmininaterViewModel.PrivilegedUserViewModel();
            var selectedUser = AdmininaterTools.GetUserById(id);
            privilegedUserViewModel.Id = id;
            privilegedUserViewModel.Username = selectedUser.Username;
            privilegedUserViewModel.Email = selectedUser.Email;
            privilegedUserViewModel.CreatedTimestamp = selectedUser.CreatedTimestamp;
            privilegedUserViewModel.IsAdmininater = selectedUser.IsAdmininater;

            return PartialView("_PrivilegedUserEditModal", privilegedUserViewModel);

        }

        [HttpPost]
        [Route("SaveUserChanges")]
        public IActionResult SaveUserChanges(long id, string username, string email, string password)
        {
            var user = AdmininaterTools.GetUserById(id);
            if (user.IsAdmininater)
                return Json(false);

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username))
                return Json(false);

            if (UserHandler.CheckUserExists(username, email, id))
                return Json(false);
            UserHandler.EditUserDetails(id, email, username);

            if (password != null)
            {
                var salt = PasswordHasher.GenerateNewSalt();
                var hashedPassword = PasswordHasher.ComputeHash(password, Encoding.ASCII.GetBytes(salt));
                UserHandler.UpdatePassword(UserId.Value, hashedPassword, salt);
                return Json(true);
            }
            return Json(true);
        }

        [HttpPost]
        [Route("CreateNewUser")]
        public IActionResult CreateNewUser(string username, string email, string password, bool isAdmininater)
        {
            if (email == null || username == null || password == null)
                return Json(false);

            SignUpResult validateResult = AuthUserManager.SignUp(username, email, password, isAdmininater);

            if (validateResult.User == null)
                return Json(false);

            return Json(true);
        }

        [HttpGet]
        [Route("ShowUserModal")]
        public IActionResult ShowUserModal()
        {
            return PartialView("_PrivilegedUserAddModal");
        }

        [HttpPost]
        [Route("Ban")]
        public IActionResult Ban(long id)
        {
            var admininaterId = UserId.Value;
            if (id == admininaterId)
                return Json(false);
            AdmininaterTools.Ban(id, admininaterId);
            return Json(true);
        }

        [HttpPost]
        [Route("Unban")]
        public IActionResult Unban(long id)
        {
            AdmininaterTools.Unban(id);
            return Json(true);
        }
    }
}
