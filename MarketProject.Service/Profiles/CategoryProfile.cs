using AutoMapper;
using MarketProject.Models.Dtos.Categories;
using MarketProject.Models.Entities;

namespace MarketProject.Service.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<CategoryAddRequestDto, Category>();
        CreateMap<CategoryUpdateRequestDto, Category>();
    }
}
