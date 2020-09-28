using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager_MVC.Models;
using EmployeeManager_MVC.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;

namespace EmployeeManager_MVC.Controllers
{
    public class SecurityController : Controller
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;
        private readonly SignInManager<AppIdentityUser> signinManager;

        public SecurityController(UserManager<AppIdentityUser> userManager,
            RoleManager<AppIdentityRole> roleManager,
            SignInManager<AppIdentityUser> signinManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // First Register() is called when "create new account link" is clicked from sign in page.
        // It displays a blank user registration page.
        public IActionResult Register()
        {
            return View();
        }

        // This second Register() is invoked when you submit the user registration page by
        // entering the details and clicking the Create button.
        // It receices a Register obj through model binding.
        [HttpPost]
        public IActionResult Register(Register obj)
        {
            // If model contains valid values
            if (ModelState.IsValid)
            {

                if (!roleManager.RoleExistsAsync("Manager").Result)
                {
                    AppIdentityRole role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Manager can perform CRUD operations.";
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }

                AppIdentityUser user = new AppIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.FullName = obj.FullName;
                user.BirthDate = obj.BirthDate;

                IdentityResult result = userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                    return RedirectToAction("SignIn", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User details");
                }
            }

            return View(obj);

            }
        

        public IActionResult SignIn()
        {
        return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignIn obj)
        {
             if (ModelState.IsValid)
               {
            var result = signinManager.PasswordSignInAsync
                (obj.UserName, obj.Password, obj.RememberMe, false).Result;
            //var result = signinManager.PasswordSignInAsync(
            //obj.UserName, obj.Password, obj.RememberMe).Result;

            if (result.Succeeded)
                {
                    return RedirectToAction("List", "EmployeeManager");
                 }
                else
                {
                ModelState.AddModelError("", " InvalidCastException user details");
                }
            }

        return View(obj);
        }

        [HttpPost]
        public IActionResult SignOut()
        {
            return View();
        }
    }
}
