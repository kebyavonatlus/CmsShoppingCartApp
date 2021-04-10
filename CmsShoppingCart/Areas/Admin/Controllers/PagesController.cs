using CmsShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CmsShoppingCart.Models;

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

        // GET /admin/pages/details/1
        public async Task<IActionResult> Details(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET /admin/pages/create
        public IActionResult Create() => View();

        // POST /admin/create
        [HttpPost]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The title already exists.");
                    return View(page);
                }

                _context.Add(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Page has been added!";

                return RedirectToAction("Index");
            }

            return View(page);
        }
    }
}
