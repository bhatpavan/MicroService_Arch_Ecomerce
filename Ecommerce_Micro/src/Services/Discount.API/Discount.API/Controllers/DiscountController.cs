using Discount.API;
using Discount.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("discount/v1/")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository discountRepositary;
        private readonly ILogger<DiscountController> logger;

        public DiscountController(IDiscountRepository discountRepositary, ILogger<DiscountController> logger)
        {
            this.discountRepositary = discountRepositary;
            this.logger = logger;
        }

        [HttpGet("{productName}")]
        [ProducesResponseType(200, Type = typeof(Coupon))]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            return Ok(await discountRepositary.GetCoupon(productName));
        }

        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpDelete]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            return Ok(await discountRepositary.DeleteCoupon(productName));
        }

        [ProducesResponseType(200, Type = typeof(Coupon))]
        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            if (await discountRepositary.CreateCoupon(coupon))
            {
                return Ok(((OkObjectResult)GetDiscount(coupon.ProductName).Result).Value);
            }

            return Ok(false);
        }

        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await discountRepositary.UpdateCoupon(coupon));
        }

    }
}
