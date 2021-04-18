using CmsShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CmsShoppingCartContext _context;
        public ProductsController(CmsShoppingCartContext context)
        {
            _context = context;
        }

        // GET /products
        public async Task<IActionResult> Index(int p = 1)
        {
            var pageSize = 6;
            var products = _context.Products.OrderByDescending(x => x.Id)
                    .Skip((p - 1) * pageSize)
                    .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Count() / pageSize);

            return View(await products.ToListAsync());
        }

        // GET /products/category
        public async Task<IActionResult> ProductsByCategory(string categorySlug, int p = 1)
        {
            var category = await _context.Categories.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();

            if (category == null)
            {
                RedirectToAction("Index");
            }

            var pageSize = 2;
            var products = _context.Products.OrderByDescending(x => x.Id)
                    .Where(x => x.CategoryId == category.Id)
                    .Skip((p - 1) * pageSize)
                    .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Where(x => x.CategoryId == category.Id).Count() / pageSize);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = category.Slug;

            return View(await products.ToListAsync());
        }
    }
}
