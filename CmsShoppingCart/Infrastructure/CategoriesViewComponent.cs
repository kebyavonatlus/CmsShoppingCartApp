using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructure
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly CmsShoppingCartContext _context;

        public CategoriesViewComponent(CmsShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View(categories);
        }

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.OrderBy(x => x.Sorting).ToListAsync();
        }

    }
}
