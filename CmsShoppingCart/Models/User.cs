namespace CmsShoppingCart.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        User() { }
        public User(AppUser appUser)
        {
            UserName = appUser.UserName;
            Email = appUser.Email;
            Password = appUser.PasswordHash;
        }
    }
}
