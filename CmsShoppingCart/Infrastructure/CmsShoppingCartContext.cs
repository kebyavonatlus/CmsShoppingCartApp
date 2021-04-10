using CmsShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Infrastructure
{
    public class CmsShoppingCartContext : DbContext
    {
        public CmsShoppingCartContext(DbContextOptions<CmsShoppingCartContext> options) : base (options)
        {

        }

        public DbSet<Page> Pages { get; set; }
    }
}
