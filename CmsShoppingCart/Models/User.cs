using System.ComponentModel.DataAnnotations;

namespace CmsShoppingCart.Models
{
    public class User
    {
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string UserName { get; set; }

        [EmailAddress, Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User() { }
    }
}
