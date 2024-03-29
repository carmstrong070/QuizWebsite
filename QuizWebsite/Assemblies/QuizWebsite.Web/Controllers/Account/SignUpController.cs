﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizWebsite.Web.Authentication;
using QuizWebsite.Web.Models.Account;

namespace QuizWebsite.Web.Controllers.Account
{
    [Area("Account")]
    public class SignUpController : AuthenticatedControllerBase
    {
        public SignUpController(IUserManager authUserManager) : base(authUserManager) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("SignUp")]
        public IActionResult SignUp()
        {
            var vm = new SignUpViewModel();
            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(SignUpViewModel vm)
        {
            if (vm.Email == null || vm.Username == null || vm.Password == null)
                return View(vm);

            SignUpResult validateResult = AuthUserManager.SignUp(vm.Username, vm.Email, vm.Password);

            if (validateResult.User == null)
                return View(vm);

            AuthUserManager.SignIn(HttpContext, validateResult.User, true);
            return RedirectToAction("QuizPortal", "QuizPortal");

        }
    }
}

