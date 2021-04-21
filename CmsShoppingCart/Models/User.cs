using Microsoft.AspNetCore.Identity;

namespace CmsShoppingCart.Models
{
    public class User : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
