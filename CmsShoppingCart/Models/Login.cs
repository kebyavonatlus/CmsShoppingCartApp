using System.ComponentModel.DataAnnotations;

namespace CmsShoppingCart.Models
{
    public class Login
    {
        [EmailAddress, Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
