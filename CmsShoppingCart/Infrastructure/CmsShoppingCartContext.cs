using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Infrastructure
{
    public class CmsShoppingCartContext : DbContext
    {
        public CmsShoppingCartContext(DbContextOptions<CmsShoppingCartContext> options) : base (options)
        {

        }
    }
}
