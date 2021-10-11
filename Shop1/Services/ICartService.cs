using Shop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Services
{
    public interface ICartService
    {
        bool CartExists(int userId);
        bool AddToCart(int productId, int cartId);
        bool CheckOut(int userId, string deliveryAddress);
        Cart GetCart(int userId);
        Cart AddCart(int userId);
        List<CartProduct> GetCartProducts(int cartId);
        bool RemoveProductFromCart(int cartId, int productId);
        List<Cart> GetUserCarts(int userId);
        List<Cart> GetCarts();
    }
}
