using Microsoft.AspNetCore.Identity;

namespace CmsShoppingCart.Models
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
