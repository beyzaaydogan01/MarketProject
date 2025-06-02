
using AutoMapper;
using MarketProject.Models.Dtos.Sales;
using MarketProject.Models.Entities;

namespace MarketProject.Service.Profiles;

public class SalesProfile : Profile
{
    public SalesProfile()
    {

        CreateMap<Sales, SalesResponseDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice)); ;

        CreateMap<SalesAddRequestDto, Sales>().ReverseMap();
        CreateMap<SalesUpdateRequestDto, Sales>().ReverseMap();
    }

}
