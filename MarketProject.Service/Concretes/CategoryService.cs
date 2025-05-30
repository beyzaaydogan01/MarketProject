using AutoMapper;
using MarketProject.Models.Dtos.Categories;
using MarketProject.Models.Entities;
using MarketProject.Repository.Repositories.Abstracts;
using MarketProject.Service.Abstracts;


namespace MarketProject.Service.Concretes;

public sealed class CategoryService(IMapper mapper, ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task AddAsync(CategoryAddRequestDto categoryAddRequestDto)
    {
        Category category = mapper.Map<Category>(categoryAddRequestDto);
        await categoryRepository.AddAsync(category);
    }

    public async Task UpdateAsync(CategoryUpdateRequestDto categoryUpdateRequestDto)
    {
        Category category = mapper.Map<Category>(categoryUpdateRequestDto);
        await categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await categoryRepository.GetAsync(x => x.Id == id, enableTracking: true, include: false);

        if (category == null)
        {
            throw new Exception($"Silinmek istenen kategori bulunamadı (id: {id})");
            // veya: return; ya da: return NotFound();
        }

        await categoryRepository.DeleteAsync(category);
    }


    public async Task<Category> GetByIdForUpdateAsync(int id)
    {
        var category = await categoryRepository.GetAsync(x => x.Id == id, include: false);

        if (category is null)
        {
            throw new InvalidOperationException($"Kategori bulunamadı. Id: {id}");
        }

        return category!;
    }

    public async Task<CategoryResponseDto> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetAsync(x => x.Id == id, enableTracking: false, include: false);
        var response = mapper.Map<CategoryResponseDto>(category);
        return response;
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync(enableTracking: false, include: false);
        var responses = mapper.Map<List<CategoryResponseDto>>(categories);
        return responses;
    }

}