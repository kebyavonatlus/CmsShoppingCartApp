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

        // GET /admin/pages
        public async Task<IActionResult> Index()
        {
            var pages = from p in _context.Pages
                        orderby p.Sorting
                        select p;

            var pagesList = await pages.ToListAsync();

            return View(pagesList);
        }

        // GET /admin/details/1
        public async Task<IActionResult> Details(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }
    }
}
