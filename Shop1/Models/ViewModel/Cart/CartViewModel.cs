using Shop1.Models.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Models.ViewModel.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public List<ProductIndexViewModel> Products { get; set; }

        public CartViewModel()
        {
            Products = new List<ProductIndexViewModel>();
        }
    }
}
