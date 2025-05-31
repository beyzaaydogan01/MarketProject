
using MarketProject.Models.Dtos.Users;

namespace MarketProject.Service.Abstracts;

public interface IUserService
{
    Task<UserResponseDto> CreateUserAsync(RegisterRequestDto register);
    Task<UserResponseDto> LoginAsync(LoginRequestDto login);
}