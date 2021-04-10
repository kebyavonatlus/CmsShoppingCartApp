using System.ComponentModel.DataAnnotations;

namespace CmsShoppingCart.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я-]+$", ErrorMessage = "Only letter are alowed")]
        public string Name { get; set; }

        public string Slug { get; set; }

        public int Sorting { get; set; }
    }
}
