
using MarketProject.Models.Dtos.Categories;
using MarketProject.Models.Entities;

namespace MarketProject.Service.Abstracts;

public interface ICategoryService
{
    Task AddAsync(CategoryAddRequestDto categoryAddRequestDto);
    Task UpdateAsync(CategoryUpdateRequestDto categoryUpdateRequestDto);
    Task DeleteAsync(int id);
    Task<Category> GetByIdForUpdateAsync(int id);
    Task<CategoryResponseDto> GetByIdAsync(int id);
    Task<List<CategoryResponseDto>> GetAllAsync();
}