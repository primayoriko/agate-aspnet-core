using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agate_View.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Agate_View.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<StudentUser> _userManager;
        private SignInManager<StudentUser> _signInManager;

        public AuthController(UserManager<StudentUser> userManager, SignInManager<StudentUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Register(/*[Bind("StudentID","Email","Name","UserName","Password")]*/[FromForm]StudentUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var std = new StudentUser { 
                StudentID = user.StudentID,
                Email = user.Email,
                Name = user.Name,
                UserName = user.UserName,
            };

            var res = await _userManager.CreateAsync(std, user.Password);

            if (res.Succeeded)
            {
                //await _signInManager.SignInAsync(std, false);
                return Redirect("/");

            } else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] StudentUser user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach(var i in errors)
                {
                    ModelState.AddModelError("", i.ErrorMessage);
                }
                return View();
            }

            var res = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.Remember, false);
            if (!res.Succeeded)
            {
                /*foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }*/
                ModelState.AddModelError("", "Invalid login attempt");
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
