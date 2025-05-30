using Core.Repositories;
using MarketProject.Models.Entities;
using MarketProject.Repository.Contexts;
using MarketProject.Repository.Repositories.Abstracts;

namespace MarketProject.Repository.Repositories.Concretes;

public sealed class ProductRepository : EfRepositoryBase<Product, int, BaseDbContext>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }
}