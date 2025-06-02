using Core.Repositories;
using MarketProject.Models.Entities;
using MarketProject.Repository.Contexts;
using MarketProject.Repository.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace MarketProject.Repository.Repositories.Concretes;

public sealed class ProductRepository : EfRepositoryBase<Product, int, BaseDbContext>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }
    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await Context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category) 
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal min, decimal max, CancellationToken cancellationToken = default)
    {
        return await Context.Products
            .Where(x => x.Price >= min && x.Price <= max)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

}