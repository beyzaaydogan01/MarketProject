using AutoMapper;
using MarketProject.Models.Dtos.Users;
using MarketProject.Models.Entities;
using MarketProject.Service.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace MarketProject.Service.Concretes;

public sealed class UserService(UserManager<User> userManager, IMapper mapper) : IUserService
{
    public async Task<UserResponseDto> CreateUserAsync(RegisterRequestDto register)
    {
        User user = mapper.Map<User>(register);
        var result = await userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            // todo: ilgili hata alınırsa exception fırlat 
        }

        UserResponseDto dto = mapper.Map<UserResponseDto>(user);
        return dto;
    }

    public async Task<UserResponseDto> LoginAsync(LoginRequestDto login)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user is null)
        {
            // todo: ilgili hata alınırsa exception fırlat 
        }

        var passwordIsMatch = await userManager.CheckPasswordAsync(user, login.Password);
        if (passwordIsMatch is false)
        {
            // todo: ilgili hata alınırsa exception fırlat 
        }

        UserResponseDto dto = mapper.Map<UserResponseDto>(user);
        return dto;
    }
}