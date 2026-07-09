using Emarket.Core.Aplication.Interface;
using Emarket.Core.Aplication.Interface.Repositories;
using Emarket.Infrastructure.Persistence.Contexts;
using Emarket.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistence
{
   public static class ServicesRegistrationContext
    {
        public static void AddPertenceInfrastructure(this IServiceCollection service, IConfiguration confi)
        {
            #region Context
            if (confi.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<EmarketContext>(options => options.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                string connectionString = confi.GetConnectionString("DefaultConnection");

                // Retrieve environment variables at runtime to build the connection string dynamically
                var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "db,1433";
                var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "EmarketDb";
                var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
                var dbPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD") ?? Environment.GetEnvironmentVariable("DB_PASSWORD");

                if (!string.IsNullOrEmpty(dbPassword))
                {
                    connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";
                }

                service.AddDbContext<EmarketContext>(options =>
                options.UseSqlServer(connectionString
                , m => m.MigrationsAssembly(typeof(EmarketContext).Assembly.FullName)));
            }
            #endregion


            #region repositories 
            service.AddTransient(typeof(IGenericReposistory<>), typeof(GenericRepository<>));
            service.AddTransient<IAddsRepository, AddsRepositoty>();
            service.AddTransient<ICategoryRepository, CategoriesRepository>();
            service.AddTransient<IUserRepository, UserRepository>();

            #endregion
        }
    }
}
