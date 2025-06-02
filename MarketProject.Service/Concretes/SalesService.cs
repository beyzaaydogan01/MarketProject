using AutoMapper;
using MarketProject.Models.Dtos.Sales;
using MarketProject.Models.Entities;
using MarketProject.Repository.Repositories.Abstracts;
using MarketProject.Repository.Repositories.Concretes;
using MarketProject.Service.Abstracts;

namespace MarketProject.Service.Concretes;

public class SalesService (ISalesRepository salesRepository, IMapper mapper,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository) : ISalesService
{

    public async Task AddAsync(SalesAddRequestDto addRequestDto, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetAsync(x => x.Id == addRequestDto.ProductId, cancellationToken: cancellationToken);

        if (product == null)
            throw new Exception("Ürün bulunamadı");

        if (product.Stock < addRequestDto.Quantity)
            throw new Exception("Yeterli stok yok");

        product.Stock -= addRequestDto.Quantity;
        await productRepository.UpdateAsync(product, cancellationToken);

        Sales sales = mapper.Map<Sales>(addRequestDto);
        sales.TotalPrice = addRequestDto.Quantity * addRequestDto.UnitPrice;
        sales.SalesDate = DateTime.Now;

        await salesRepository.AddAsync(sales, cancellationToken);
    }

    public async Task UpdateAsync(SalesUpdateRequestDto salesUpdateRequestDto, CancellationToken cancellationToken = default)
    {
        var sales = await salesRepository.GetAsync(x => x.Id == salesUpdateRequestDto.Id, include: false, cancellationToken: cancellationToken);

        if (sales is null)
        {
            throw new InvalidOperationException($"Satış bulunamadı. Id: {salesUpdateRequestDto.Id}");
        }

        Sales updated = mapper.Map(salesUpdateRequestDto, sales);
        await salesRepository.UpdateAsync(updated, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var sales = await salesRepository.GetAsync(x => x.Id == id, include: false, cancellationToken: cancellationToken);

        if (sales is null)
        {
            throw new InvalidOperationException($"Satış bulunamadı. Id: {id}");
        }

        await salesRepository.DeleteAsync(sales, cancellationToken);
    }

    public async Task<List<SalesResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        List<Sales> salesList = await salesRepository.GetAllAsync(include: true, cancellationToken: cancellationToken);
        return mapper.Map<List<SalesResponseDto>>(salesList);
    }

    public async Task<Sales> GetByIdForUpdateAsync(int id)
    {
        var sales = await salesRepository.GetAsync(x => x.Id == id, include: false);

        if (sales is null)
        {
            throw new InvalidOperationException($"Satış bulunamadı. Id: {id}");
        }

        return sales;
    }
}