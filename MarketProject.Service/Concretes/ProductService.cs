
using AutoMapper;
using MarketProject.Models.Dtos.Products;
using MarketProject.Models.Entities;
using MarketProject.Repository.Repositories.Abstracts;
using MarketProject.Repository.Repositories.Concretes;
using MarketProject.Service.Abstracts;
using MarketProject.Service.Helpers.Cloudinary;
using Microsoft.EntityFrameworkCore;

namespace MarketProject.Service.Concretes;

public sealed class ProductService(IMapper mapper, 
    IProductRepository productRepository,
    ISalesRepository salesRepository,
    IFileService fileService) : IProductService
{

    public async Task AddAsync(ProductAddRequestDto addRequestDto, CancellationToken cancellationToken = default)
    {
        Product product = mapper.Map<Product>(addRequestDto);
        string url = await fileService.UploadImageAsync(addRequestDto.File, "products-image");
        product.ImageUrl = url;
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
            throw new InvalidOperationException($"Ürün bulunamadı. Id: {productUpdateRequestDto.Id}");
        }
        Product updated = mapper.Map(productUpdateRequestDto, product!);
        await productRepository.UpdateAsync(updated, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            throw new InvalidOperationException("Silinmek istenen ürün bulunamadı.");

        bool hasSales = await salesRepository.HasSalesForProductAsync(id, cancellationToken);
        if (hasSales)
            throw new InvalidOperationException("Bu ürüne ait satış kaydı bulunduğundan silinemez.");

        await productRepository.DeleteAsync(product, cancellationToken);
    }


    public async Task<List<ProductResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        List<Product> products = await productRepository.GetAllAsync(include:true, cancellationToken: cancellationToken);
        List<ProductResponseDto> responses = mapper.Map<List<ProductResponseDto>>(products);

        return responses;
    }

    public async Task<List<ProductResponseDto>> GetAllByCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByCategoryIdAsync(categoryId, cancellationToken);
        return mapper.Map<List<ProductResponseDto>>(products);
    }

    public async Task<List<ProductResponseDto>> GetAllByPriceRangeAsync(decimal min, decimal max, CancellationToken cancellationToken = default)
    {
        var products = await productRepository.GetProductsByPriceRangeAsync(min, max, cancellationToken);
        return mapper.Map<List<ProductResponseDto>>(products);
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