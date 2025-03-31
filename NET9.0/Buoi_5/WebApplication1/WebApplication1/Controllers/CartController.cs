using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        const string CARTKEY = "cart";
        List<CartItem> GetCart()
        {
            var session = HttpContext.Session.GetString(CARTKEY);
            return string.IsNullOrEmpty(session)
            ? new List<CartItem>()
            : JsonConvert.DeserializeObject<List<CartItem>>(session) ??

            new();
        }
        void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString(CARTKEY,

            JsonConvert.SerializeObject(cart));
        }
        // GET: CartController
        public ActionResult Index()
        {
            var cart = GetCart();
            ViewBag.Total = cart.Sum(i => i.Price * i.Quantity);
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.Id == id);
            if (item != null) item.Quantity++;
            else cart.Add(new CartItem
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
            });
            SaveCart(cart);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.Id == id);
            if (item != null) cart.Remove(item);
            SaveCart(cart);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            SaveCart(new List<CartItem>());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.Id == productId);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetString(CARTKEY);
            var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart) ?? new();
            if (!cartItems.Any())
                return RedirectToAction("Index", "Product");
            var order = new Order();
            order.OrderDate = DateTime.Now;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
                if (userId != null)
                {
                    order.User = _context.Users.Find(userId);
                }
            }
            order.TotalAmount = cartItems.Sum(i => i.Price * i.Quantity);
            order.OrderDetails = cartItems.Select(item => new OrderDetail
            {
                ProductId = item.Id,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();
            _context.Order.Add(order);
            _context.SaveChanges();
            // Xóa giỏ hàng
            HttpContext.Session.Remove(CARTKEY);
            ViewBag.TotalAmount = order.TotalAmount;
            return View("Success");
        }
        [HttpPost]
        public IActionResult UpdateCartItem(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.Id == productId);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    cart.Remove(item);
                }
                SaveCart(cart);
                return Json(new { success = true, total = cart.Sum(i => i.Price * i.Quantity) });
            }
            return Json(new { success = false });
        }
    }
}
