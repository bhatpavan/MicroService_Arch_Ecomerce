using Dapper;
using Discount.API.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private NpgsqlConnection npsqlConnection;
        public DiscountRepository(IConfiguration config)
        {
            npsqlConnection = new NpgsqlConnection(config.GetValue<string>("Database:ConnectionString"));
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
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

        public async Task<Coupon> GetCoupon(string productName)
        {
            var coupon = await npsqlConnection.QueryFirstOrDefaultAsync<Coupon>("select * from  Coupon where ProductName= @ProductName", new { ProductName = productName });
            if (coupon == null)
            {
                return new Coupon() { Amount = 0, ProductName = productName, Description = "No Discount" };
            }
            return coupon;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
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
