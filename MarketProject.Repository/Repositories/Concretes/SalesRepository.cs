using Core.Repositories;
using MarketProject.Models.Entities;
using MarketProject.Repository.Contexts;
using MarketProject.Repository.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace MarketProject.Repository.Repositories.Concretes;

public sealed class SalesRepository : EfRepositoryBase<Sales, int, BaseDbContext>, ISalesRepository
{
    public SalesRepository(BaseDbContext context) : base(context)
    {
    }

    public override async Task<List<Sales>> GetAllAsync(Expression<Func<Sales, bool>>? filter = null, bool include = true, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Sales> query = Context.Set<Sales>();

        if (filter != null)
            query = query.Where(filter);

        if (include)
        {
            query = query
                .Include(s => s.Product)
                .ThenInclude(p => p.Category);
        }

        if (!enableTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<Sales, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Context.Sales.AnyAsync(predicate, cancellationToken);
    }

    public async Task<bool> HasSalesForProductAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await Context.Sales.AnyAsync(s => s.ProductId == productId, cancellationToken);
    }

}
