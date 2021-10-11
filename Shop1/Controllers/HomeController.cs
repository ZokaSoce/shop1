using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop1.Models;
using Shop1.Models.ViewModel.Home;
using Shop1.Models.ViewModel.Product;
using Shop1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetProducts();
            var productsVm = new List<ProductIndexViewModel>();
            foreach (var item in products)
            {
                productsVm.Add(new ProductIndexViewModel { Description = item.Description, Id = item.Id, Image = "/files/" + item.Image, Name = item.Name, Price = item.Price });
            }
            var model = new HomeIndexViewModel();
            model.Products = productsVm;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
