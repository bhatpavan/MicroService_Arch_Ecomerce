using Basket.API.Models;
using Basket.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("Basket/v1/")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepositary;
        private readonly ILogger<BasketController> logger;

        public BasketController(IBasketRepository basketRepositary, ILogger<BasketController> logger)
        {
            this.basketRepositary = basketRepositary;
            this.logger = logger;
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [HttpGet("{userName}")]
        [ProducesResponseType(200, Type = typeof(ShoppingCart))]
        public async Task<IActionResult> GetBasket(string userName)
        {
            return Ok(await basketRepositary.GetBasket(userName));
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [HttpPost("UpdateBasket")]
        [ProducesResponseType(200, Type = typeof(ShoppingCart))]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shopingCart)
        {
            return Ok(await basketRepositary.UpdateBasket(shopingCart));

        }

        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(200, Type = typeof(void))]
        [HttpDelete("Delete/{userName}")]

        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await basketRepositary.DeleteBasket(userName);
            return Ok();
        }
    }
}
