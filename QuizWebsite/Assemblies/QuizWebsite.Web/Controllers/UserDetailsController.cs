using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models;
using System.Text;

namespace QuizWebsite.Web.Controllers
{
    public class UserDetailsController : AuthenticatedControllerBase
    {
        public UserDetailsController(IUserManager authUserManager) : base(authUserManager) { }

        [HttpGet]
        public IActionResult Edit()
        {
            var user = UserHandler.GetUserById(UserId.Value);
            var userViewModel = new UserDetailsViewModel();
            userViewModel.Username = user.Username;
            userViewModel.Email = user.Email;
            return View("UserDetails", userViewModel);
        }

        [HttpPost]
        public IActionResult Edit(UserDetailsViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Email) || string.IsNullOrWhiteSpace(vm.Username))
                return RedirectToAction("QuizPortal", "QuizPortal");

            if (UserHandler.CheckUserExists(vm.Username, vm.Email, UserId))
                return RedirectToAction("QuizPortal", "QuizPortal");
            UserHandler.EditUserDetails(UserId.Value, vm.Email, vm.Username);

            if (vm.Password != null)
            {
                var salt = PasswordHasher.GenerateNewSalt();
                var hashedPassword = PasswordHasher.ComputeHash(vm.Password, Encoding.ASCII.GetBytes(salt));
                UserHandler.UpdatePassword(UserId.Value, hashedPassword, salt);
                return RedirectToAction("QuizPortal", "QuizPortal");
            }
            return RedirectToAction("QuizPortal", "QuizPortal");
        }
    }
}
