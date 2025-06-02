using MarketProject.Repository.Contexts;
using MarketProject.Repository.Repositories.Abstracts;
using MarketProject.Repository.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketProject.Repository;

public static class RepositoryDependencies
{
    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISalesRepository, SalesRepository>();

        services.AddDbContext<BaseDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
