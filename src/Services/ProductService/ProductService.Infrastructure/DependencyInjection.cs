using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Abstractions.Persistence;
using ProductService.Infrastructure.Persistence;
using ProductService.Infrastructure.Repositories;

namespace ProductService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("ProductDatabase")
                ?? throw new InvalidOperationException(
                    "Product database connection string is missing.");

            services.AddDbContext<ProductDbContext>(
                options =>
                    options.UseNpgsql(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}