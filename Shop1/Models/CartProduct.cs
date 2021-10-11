using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Models
{
    public class CartProduct
    {

        public int CartId { get; set; }
        public int ProductId { get; set; }
        //public int Quantity { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public CartProduct()
        {
            //Cart = new Cart();
            //Product = new Product();
        }
    }
}
