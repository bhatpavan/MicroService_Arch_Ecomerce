using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migration is started");
                    using var npsqlConnection = new NpgsqlConnection(config.GetValue<string>("Database:ConnectionString"));
                    npsqlConnection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = npsqlConnection,
                    };
                    command.CommandText = "DROP Table if Exists Coupon";
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
            }
            return host;
        }
    }
}
