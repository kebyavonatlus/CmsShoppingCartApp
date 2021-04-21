using System.ComponentModel.DataAnnotations;

namespace CmsShoppingCart.Models
{
    public class UserEdit
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(4, ErrorMessage = "Minimum length is 4")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserEdit() { }
        public UserEdit(AppUser appUser)
        {
            Email = appUser.Email;
            Password = appUser.PasswordHash;
        }
    }
}
