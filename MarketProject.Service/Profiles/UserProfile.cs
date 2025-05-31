using AutoMapper;
using MarketProject.Models.Dtos.Users;
using MarketProject.Models.Entities;

namespace MarketProject.Service.Profiles;

public class UserProfile : Profile
{
    public UserProfile() 
    {
        CreateMap<RegisterRequestDto, User>();
        CreateMap<User, UserResponseDto>();
    }
}
