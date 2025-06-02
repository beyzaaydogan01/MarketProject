using MarketProject.Models.Dtos.Sales;
using MarketProject.Models.Entities;

namespace MarketProject.Service.Abstracts;

public interface ISalesService
{
    Task AddAsync(SalesAddRequestDto addRequestDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(SalesUpdateRequestDto salesUpdateRequestDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<List<SalesResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Sales> GetByIdForUpdateAsync(int id);
}
