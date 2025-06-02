using MarketProject.Models.Dtos.Products;
using MarketProject.Models.Entities;

namespace MarketProject.Service.Abstracts;

public interface IProductService
{
    Task AddAsync(ProductAddRequestDto addRequestDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<List<ProductResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<ProductResponseDto>> GetAllByCategoryId(int categoryId, CancellationToken cancellationToken = default);
    Task<List<ProductResponseDto>> GetAllByPriceRangeAsync(decimal min, decimal max, CancellationToken cancellationToken = default);
    Task<Product> GetByIdForUpdateAsync(int id);
}