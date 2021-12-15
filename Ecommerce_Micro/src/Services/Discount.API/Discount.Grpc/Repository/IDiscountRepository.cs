using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using System.Threading.Tasks;

namespace Discount.Grpc
{
    public interface IDiscountRepository
    {
        Task<CouponModel> GetCoupon(string productName);
        Task<bool> CreateCoupon(CouponModel coupon);
        Task<bool> UpdateCoupon(CouponModel coupon);
        Task<bool> DeleteCoupon(string productName);

    }
}
