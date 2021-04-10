using CmsShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly CmsShoppingCartContext _context;
        public PagesController(CmsShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pages = from p in _context.Pages
                        orderby p.Sorting
                        select p;

            var pagesList = await pages.ToListAsync();

            return View(pagesList);
        }
    }
}
