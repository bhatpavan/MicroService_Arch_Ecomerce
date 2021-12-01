using Catalog.API.Data;
using Catalog.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepositary
    {
        private readonly ICatalogContext catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }
        public async Task CreateProduct(Product product)
        {
            await catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await catalogContext.Products.DeleteOneAsync(x => x.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await (await catalogContext.Products.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByCatagory(string catagory)
        {
            return await (await catalogContext.Products.FindAsync(x => string.Equals(x.Category, catagory, StringComparison.OrdinalIgnoreCase))).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await (await catalogContext.Products.FindAsync(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await (await catalogContext.Products.FindAsync(x => true)).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await catalogContext.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
