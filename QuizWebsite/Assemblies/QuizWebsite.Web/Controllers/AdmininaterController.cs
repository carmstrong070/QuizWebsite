using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Data;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models;

namespace QuizWebsite.Web.Controllers
{
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
    }
}
