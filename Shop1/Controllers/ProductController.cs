using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop1.Data;
using Shop1.Helpers;
using Shop1.Models;
using Shop1.Models.ViewModel;
using Shop1.Services;
using System;
using System.Linq;

namespace Shop1.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UploadImage _uploadImage;

        public ProductController(IProductService productService, UploadImage uploadImage)
        {
            _productService = productService;
            _uploadImage = uploadImage;
        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            try
            {
                var products = _productService.GetProducts();
                return View(products);
            }
            catch (Exception e)
            {
                return View();
                throw e;
            }
        }
        [HttpGet("add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductAddViewModel productVm)
        {
            try
            {
                if (productVm == null)
                {
                    return View();
                }

                Product product = new Product
                {
                    Description = productVm.Description,
                    Name = productVm.Name,
                    Price = productVm.Price
                };
                product.Image = _uploadImage.Upload(productVm.Image);

                _productService.Add(product);

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View();
                throw e;
            }
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProduct(id);
            EditProductViewModel model = new EditProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Price = product.Price,
                Name = product.Name,
                Image = product.Image
            };



            return View(model);
        }
        [HttpPost("edit")]
        public IActionResult Edit(EditProductViewModel model)
        {
            var product = new Product
            {
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Price = model.Price
            };

            if (model.FileResource != null)
            {
                product.Image = _uploadImage.Upload(model.FileResource);
            }
            else
            {
                product.Image = model.Image;
            }

            var check = _productService.Update(product);

            if (check)
            {
                return RedirectToAction("index");
            }
            return View(model);

        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(new Product { Id = id });
            return Ok();
        }
    }
}
