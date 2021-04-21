using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManger;

        public AccountController(UserManager<AppUser> userManger)
        {
            this.userManger = userManger;
        }

        //GET /account/register
        public IActionResult Register() => View();

        //POST /account/register
        [HttpPost]
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
    }
}
