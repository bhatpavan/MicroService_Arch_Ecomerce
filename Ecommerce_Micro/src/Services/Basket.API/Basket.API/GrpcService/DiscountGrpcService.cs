using Discount.Grpc.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            this.discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetCoupon(string productName)
        {
           return await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest() { ProductName = productName });
        }
    }
}
