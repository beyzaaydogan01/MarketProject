using Core.Repositories;
using MarketProject.Models.Entities;
using MarketProject.Repository.Contexts;
using MarketProject.Repository.Repositories.Abstracts;

namespace MarketProject.Repository.Repositories.Concretes;

public sealed class CategoryRepository : EfRepositoryBase<Category, int, BaseDbContext>, ICategoryRepository
{
    public CategoryRepository(BaseDbContext context) : base(context)
    {
    }
}