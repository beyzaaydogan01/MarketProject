using MarketProject.Service.Abstracts;
using MarketProject.Service.Concretes;
using MarketProject.Service.Helpers.Cloudinary;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MarketProject.Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISalesService, SalesService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}