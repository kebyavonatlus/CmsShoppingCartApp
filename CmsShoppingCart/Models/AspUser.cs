using Microsoft.AspNetCore.Identity;

namespace CmsShoppingCart.Models
{
    public class AspUser : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
