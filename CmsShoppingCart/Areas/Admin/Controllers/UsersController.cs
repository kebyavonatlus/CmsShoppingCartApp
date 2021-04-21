using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
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
