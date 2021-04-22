using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManger;

        public UsersController(UserManager<AppUser> userManger)
        {
            this.userManger = userManger;
        }

        public IActionResult Index()
        {
            return View(userManger.Users);
        }
    }
}
