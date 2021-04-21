using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManger;
        private readonly SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;

        public AccountController(UserManager<AppUser> userManger,
                                SignInManager<AppUser> signInManager,
                                IPasswordHasher<AppUser> passwordHasher)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this.passwordHasher = passwordHasher;
        }

        //GET /account/register
        [AllowAnonymous]
        public IActionResult Register() => View();

        //POST /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser()
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                var result = await userManger.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(user);
        }

        //GET /account/login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var login = new Login()
            {
                ReturnUrl = returnUrl
            };

            return View(login);
        }

        //POST /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var appUser = await userManger.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    var result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);

                    if (result.Succeeded)
                        return Redirect(login.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "Login failed, wrong credentials");
            }

            return View(login);
        }

        //GET /account/logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Redirect("/");
        }

        //GET /account/edit
        public async Task<IActionResult> Edit()
        {
            var appUser = await userManger.FindByNameAsync(User.Identity.Name);
            var user = new UserEdit(appUser);

            return View(user);
        }

        //POST /account/edit
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEdit userEdit)
        {
            var appUser = await userManger.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                appUser.Email = userEdit.Email;
                if (userEdit.Password != null)
                {
                    appUser.PasswordHash = passwordHasher.HashPassword(appUser, userEdit.Password);
                }

                var result = await userManger.UpdateAsync(appUser);
                if (result.Succeeded)
                    TempData["Success"] = "Your information has been edited!";
            }

            return View();
        }
    }
}
