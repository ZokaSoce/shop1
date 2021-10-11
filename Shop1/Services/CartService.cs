using Microsoft.EntityFrameworkCore;
using Shop1.Data;
using Shop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart AddCart(int userId)
        {
            Cart cart = new Cart
            {
                UserId = userId
            };
            _context.Carts.Add(cart);

            var check = _context.SaveChanges() > 0;
            if (check)
            {
                return cart;
            }
            return null;
        }

        public bool AddToCart(int productId, int cartId)
        {
            _context.CartProducts.Add(new Models.CartProduct { CartId = cartId, ProductId = productId });
            return _context.SaveChanges() > 0;
        }

        public bool CartExists(int userId)
        {
            return _context.Carts.FirstOrDefault(x => x.UserId == userId && x.IsDone != false) != null;
        }

        public bool CheckOut(int userId, string deliveryAddress)
        {
            var cart = _context.Carts.FirstOrDefault(x => x.UserId == userId && x.IsDone == false);
            cart.IsDone = true;
            cart.DeliveryAddress = deliveryAddress;
            _context.Carts.Update(cart);
            return _context.SaveChanges() > 0;
        }

        public Cart GetCart(int userId)
        {
            return _context.Carts.FirstOrDefault(x => x.UserId == userId && x.IsDone == false);
        }

        public List<CartProduct> GetCartProducts(int cartId)
        {
            return _context.CartProducts.Where(x => x.CartId == cartId).Include(x => x.Product).AsNoTracking().ToList();
        }

        public List<Cart> GetCarts()
        {
            return _context.Carts.Include(x => x.CartProduct)
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .ToList();
        }

        public List<Cart> GetUserCarts(int userId)
        {
            return _context.Carts.Where(x => x.UserId == userId && x.IsDone == true)
                .Include(x => x.CartProduct)
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .ToList();
        }

        public bool RemoveProductFromCart(int cartId, int productId)
        {
            var cartProduct = new CartProduct { CartId = cartId, ProductId = productId };
            _context.CartProducts.Remove(cartProduct);
            return _context.SaveChanges() > 0;
        }
    }
}
