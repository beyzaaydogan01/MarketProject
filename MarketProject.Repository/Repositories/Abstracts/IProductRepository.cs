using Core.Repositories;
using MarketProject.Models.Entities;

namespace MarketProject.Repository.Repositories.Abstracts;

public interface IProductRepository : IRepository<Product, int>
{
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<List<Product>> GetProductsByPriceRangeAsync(decimal min, decimal max, CancellationToken cancellationToken = default);
    Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
}