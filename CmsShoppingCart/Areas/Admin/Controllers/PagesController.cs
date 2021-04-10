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
        [ValidateAntiForgeryToken]
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

        // GET /admin/pages/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST /admin/edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await _context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The page already exists.");
                    return View(page);
                }

                _context.Update(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Page has been edited!";

                return RedirectToAction("Edit", new { Id = page.Id });
            }

            return View(page);
        }

        // GET /admin/pages/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
            {
                TempData["Error"] = "The page does not exist!";
            }
            else
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The page has been deleted!";
            }

            return RedirectToAction("Index");
        }

        // POST /admin/pages/reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var pageId in id)
            {
                var page = await _context.Pages.FirstAsync(x => x.Id == pageId);
                page.Sorting = count;
                _context.Update(page);
                await _context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
