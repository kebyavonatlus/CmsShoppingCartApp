using CmsShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    public class PagesController : Controller
    {
        private readonly CmsShoppingCartContext _context;
        public PagesController(CmsShoppingCartContext context)
        {
            _context = context;
        }

        // GET / or /slug
        public async Task<IActionResult> Page(string slug)
        {
            if (slug == null)
            {
                return View(await _context.Pages.Where(x => x.Slug == "home").FirstOrDefaultAsync());
            }

            var page = await _context.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }
    }
}
