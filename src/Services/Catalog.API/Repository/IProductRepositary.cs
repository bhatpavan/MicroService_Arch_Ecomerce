using Catalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repository
{
    public interface IProductRepositary
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(string id);
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductByCatagory(string catagory);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);

    }
}
