using Discount.Grpc.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository discountRepository;

        private readonly ILogger<DiscountService> logger;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
        {
            this.discountRepository = discountRepository;
            this.logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            return await discountRepository.GetCoupon(request.ProductName);
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            await discountRepository.CreateCoupon(request.Coupon);

            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return new DeleteDiscountResponse() { IsSuccess = await discountRepository.DeleteCoupon(request.ProductName) };
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            await discountRepository.UpdateCoupon(request.Coupon);
            return request.Coupon;
        }
    }
}
