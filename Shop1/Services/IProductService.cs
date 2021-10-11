using Shop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Services
{
    public interface IProductService
    {
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        Product GetProduct(int id);
        List<Product> GetProducts();
    }
}
