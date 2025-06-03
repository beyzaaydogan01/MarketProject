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
            throw new Exception($"Kullanıcı oluşturulamadı");
        }

        UserResponseDto dto = mapper.Map<UserResponseDto>(user);
        return dto;
    }

    public async Task<UserResponseDto> LoginAsync(LoginRequestDto login)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user is null)
        {
            throw new Exception("Kullanıcı bulunamadı.");
        }

        var passwordIsMatch = await userManager.CheckPasswordAsync(user, login.Password);
        if (passwordIsMatch is false)
        {
            throw new Exception("Şifre hatalı.");
        }

        UserResponseDto dto = mapper.Map<UserResponseDto>(user);
        return dto;
    }
}