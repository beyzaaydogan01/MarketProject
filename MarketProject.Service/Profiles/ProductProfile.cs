using AutoMapper;
using MarketProject.Models.Dtos.Products;
using MarketProject.Models.Entities;

namespace MarketProject.Service.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductAddRequestDto, Product>();
        CreateMap<ProductUpdateRequestDto, Product>();
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

    }
}
