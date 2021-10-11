using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop1.Models.ViewModel.Cart;
using Shop1.Models.ViewModel.Product;
using Shop1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop1.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var cart = _cartService.GetCart(userId);
                var carProduct = _cartService.GetCartProducts(cart.Id);
                var model = new CartViewModel
                {
                    Id = cart.Id,
                };

                List<ProductIndexViewModel> productViewModel = new List<ProductIndexViewModel>();
                foreach (var item in carProduct)
                {
                    productViewModel.Add(new ProductIndexViewModel { Id = item.Product.Id, Name = item.Product.Name, Price = item.Product.Price });
                }
                model.Products = productViewModel;
                return View(model);
            }
            catch (Exception e)
            {
                return View(null);
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var userClaims = User.Identity as ClaimsIdentity;

            var userId = int.Parse(userClaims.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var cart = _cartService.GetCart(userId);

            if (cart == null)
            {
                cart = _cartService.AddCart(userId);
                _cartService.AddToCart(productId, cart.Id);
            }
            _cartService.AddToCart(productId, cart.Id);
            return RedirectToAction("index", "home");
        }
        [HttpDelete]
        public IActionResult RemoveProductFromCart(int cartId, int productId)
        {
            var userClaims = User.Identity as ClaimsIdentity;

            var userId = int.Parse(userClaims.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var cart = _cartService.GetCartProducts(userId);
            bool check = false;
            if (cart.Any(x => x.ProductId == productId))
            {
                check = _cartService.RemoveProductFromCart(cartId, productId);
            }
            if (check)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("checkout")]
        public IActionResult CheckOut(int id, string deliveryAddress)
        {
            var userClaims = User.Identity as ClaimsIdentity;

            var userId = int.Parse(userClaims.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var check = _cartService.CheckOut(userId, deliveryAddress);
            return Ok();
        }

        [HttpGet("purchase")]
        public IActionResult Purchases()
        {
            var userClaims = User.Identity as ClaimsIdentity;

            var userId = int.Parse(userClaims.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var cart = _cartService.GetUserCarts(userId);
            return View(cart);
        }
        [Authorize(Roles ="admin")]
        [HttpGet("allPurchases")]
        public IActionResult AllPurchases()
        {
            var carts = _cartService.GetCarts();
            return View("Purchases",carts);
        }
    }
}
