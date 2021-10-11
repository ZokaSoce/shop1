using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Models.ViewModel.Product
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }


    }
}
