using Catalog.API.Models;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("Catalog/v1/")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepositary productRepositary;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(IProductRepositary productRepositary, ILogger<CatalogController> logger)
        {
            this.productRepositary = productRepositary;
            this.logger = logger;
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [HttpGet("Products")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await productRepositary.GetProducts());
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [HttpGet("Product/{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await productRepositary.GetProduct(id);
            if (product == null)
            {
                logger.LogError($"Product not found for id : {id}");
                return NotFound();
            }
            return Ok(product);
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [HttpGet("Product/Catagory/{catagory}")]

        public async Task<IActionResult> GetProductByCatagory(string catagory)
        {
            var product = await productRepositary.GetProductByCatagory(catagory);
            if (product == null)
            {
                logger.LogError($"Product not found for Catagory : {catagory}");
                return NotFound();
            }
            return Ok(product);
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [HttpGet("Product/Name/{name}")]
        public async Task<IActionResult> GetProductBy(string name)
        {
            var product = await productRepositary.GetProductByName(name);
            if (product == null)
            {
                logger.LogError($"Product not found for Name : {name}");
                return NotFound();
            }
            return Ok(product);
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [HttpPost("Product")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await productRepositary.CreateProduct(product);

            return Ok(CreatedAtRoute("Product", new { id = product.Id }));
        }

    }

    [ApiController]
    [Route("Catalog/v2/")]
    public class Catalog1Controller : ControllerBase
    {
        private readonly IProductRepositary productRepositary;
        private readonly ILogger<CatalogController> logger;

        public Catalog1Controller(IProductRepositary productRepositary, ILogger<CatalogController> logger)
        {
            this.productRepositary = productRepositary;
            this.logger = logger;
        }

        [ApiExplorerSettings(GroupName = "v2")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPut("Product/Update")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            return Ok(await productRepositary.UpdateProduct(product));
        }

        [ApiExplorerSettings(GroupName = "v2")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpDelete("Product/Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await productRepositary.DeleteProduct(id));
        }
    }
}
