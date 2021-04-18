using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly CmsShoppingCartContext _context;
        public CartController(CmsShoppingCartContext context)
        {
            _context = context;
        }

        // GET /cart
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };

            return View(cartVM);
        }

        // GET /cart/add/1
        public async Task<IActionResult> Add(int id)
        {
            var product = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");

            return ViewComponent("SmallCart");
        }

        // GET /cart/Decrease/1
        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            var cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        // GET /cart/Remove/1
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        // GET /cart/Clear/
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return Redirect(Request.Headers["Referer"].ToString());

            return Ok();
        }
    }
}
