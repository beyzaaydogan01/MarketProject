using Core.Repositories;
using MarketProject.Models.Entities;
using System.Linq.Expressions;

namespace MarketProject.Repository.Repositories.Abstracts;

public interface ISalesRepository : IRepository<Sales, int>
{
    Task<bool> AnyAsync(Expression<Func<Sales, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> HasSalesForProductAsync(int productId, CancellationToken cancellationToken = default);


}
