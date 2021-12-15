using Dapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace Discount.Grpc.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly NpgsqlConnection npsqlConnection;
        public DiscountRepository(IConfiguration config)
        {
            npsqlConnection = new NpgsqlConnection(config.GetValue<string>("Database:ConnectionString"));
        }

        public async Task<bool> CreateCoupon(CouponModel coupon)
        {
            var affected =
                await npsqlConnection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            var affected = await npsqlConnection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<CouponModel> GetCoupon(string productName)
        {
            var coupon = await npsqlConnection.QueryFirstOrDefaultAsync<CouponModel>("select * from  Coupon where ProductName= @ProductName", new { ProductName = productName });
            if (coupon == null)
            {
                return new CouponModel() { Amount = 0, ProductName = productName, Description = "No Discount" };
            }
            return coupon;
        }

        public async Task<bool> UpdateCoupon(CouponModel coupon)
        {
            var affected = await npsqlConnection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
