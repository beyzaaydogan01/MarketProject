
using AutoMapper;
using MarketProject.Models.Dtos.Products;
using MarketProject.Models.Entities;
using MarketProject.Repository.Repositories.Abstracts;
using MarketProject.Repository.Repositories.Concretes;
using MarketProject.Service.Abstracts;

namespace MarketProject.Service.Concretes;

public sealed class ProductService(IMapper mapper, IProductRepository productRepository) : IProductService
{

    public async Task AddAsync(ProductAddRequestDto addRequestDto, CancellationToken cancellationToken = default)
    {
        Product product = mapper.Map<Product>(addRequestDto);
        await productRepository.AddAsync(product, cancellationToken);
    }

    public async Task UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetAsync(x => x.Id == productUpdateRequestDto.Id
            , include: false,
            cancellationToken: cancellationToken
            );

        if (product is null)
        {
            //todo: Exception Fırlatılacak
        }
        Product updated = mapper.Map(productUpdateRequestDto, product!);
        await productRepository.UpdateAsync(updated, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(x => x.Id == id
            , include: false,
            cancellationToken: cancellationToken
        );

        if (product is null)
        {
            //todo: Exception Fırlatılacak
        }

        await productRepository.DeleteAsync(product!, cancellationToken);
    }

    public async Task<List<ProductResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        List<Product> products = await productRepository.GetAllAsync(include:true, cancellationToken: cancellationToken);
        List<ProductResponseDto> responses = mapper.Map<List<ProductResponseDto>>(products);

        return responses;
    }

    public async Task<List<ProductResponseDto>> GetAllByCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(
            x => x.CategoryId == categoryId,
            enableTracking: false,
            cancellationToken: cancellationToken
            );

        var responses = mapper.Map<List<ProductResponseDto>>(products);

        return responses;
    }

    public async Task<List<ProductResponseDto>> GetAllByPriceRangeAsync(decimal min, decimal max, CancellationToken cancellationToken = default)
    {
        var products = await productRepository
            .GetAllAsync(filter: x => x.Price <= max && x.Price >= min,
                enableTracking: false,
                cancellationToken: cancellationToken);

        var responses = mapper.Map<List<ProductResponseDto>>(products);
        return responses;
    }

    public async Task<Product> GetByIdForUpdateAsync(int id)
    {
        var product = await productRepository.GetAsync(x => x.Id == id, include: false);

        if (product is null)
        {
            throw new InvalidOperationException($"Ürün bulunamadı. Id: {id}");
        }

        return product!;
    }

}