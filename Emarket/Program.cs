using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Emarket.Infrastructure.Persistence.Contexts;

namespace Emarket
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Run migrations automatically on startup if not using an in-memory database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var useInMemory = configuration.GetValue<bool>("UseInMemoryDatabase");
                    if (!useInMemory)
                    {
                        var context = services.GetRequiredService<EmarketContext>();
                        
                        // Retry loop to handle SQL Server startup delay
                        int retries = 6;
                        int delaySeconds = 5;
                        while (retries > 0)
                        {
                            try
                            {
                                await context.Database.MigrateAsync();
                                Console.WriteLine("Database migrations applied successfully.");
                                break; // Success, break retry loop
                            }
                            catch (Exception ex)
                            {
                                retries--;
                                if (retries == 0)
                                {
                                    throw;
                                }
                                Console.WriteLine($"Database migration failed. Retrying in {delaySeconds} seconds... ({retries} attempts left). Error: {ex.Message}");
                                await Task.Delay(delaySeconds * 1000);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

