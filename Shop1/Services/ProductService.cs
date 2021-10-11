using Shop1.Data;
using Shop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(Product product)
        {
            if (product != null)
            {
                _context.Products.Remove(product);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool Update(Product product)
        {
            if (product != null)
            {
                _context.Products.Update(product);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
