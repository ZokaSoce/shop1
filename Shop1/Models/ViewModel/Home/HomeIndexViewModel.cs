using Shop1.Models.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Models.ViewModel.Home
{
    public class HomeIndexViewModel
    {
        public List<ProductIndexViewModel> Products { get; set; }
        public int CartProductCount { get; set; }

    }
}
