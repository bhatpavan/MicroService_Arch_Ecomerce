using Basket.API.GrpcService;
using Basket.API.Models;
using Basket.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("Basket/v1/")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepositary;
        private readonly ILogger<BasketController> logger;
        private readonly DiscountGrpcService discountGrpcService;

        public BasketController(IBasketRepository basketRepositary, ILogger<BasketController> logger, DiscountGrpcService discountGrpcService)
        {
            this.basketRepositary = basketRepositary;
            this.logger = logger;
            this.discountGrpcService = discountGrpcService;
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
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            // communicate with Discount Grpc
            foreach(var item in shoppingCart.Items)
            {
                var coupon = await discountGrpcService.GetCoupon(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await basketRepositary.UpdateBasket(shoppingCart));

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
