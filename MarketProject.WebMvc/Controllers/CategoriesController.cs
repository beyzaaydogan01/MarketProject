using MarketProject.WebMvc.Models.ViewModels;
using MarketProject.Models.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using MarketProject.Service.Abstracts;
using MarketProject.Service.Concretes;
using Microsoft.AspNetCore.Authorization;

namespace MarketProject.WebMvc.Controllers;

[Authorize]
public sealed class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CategoryAddRequestDto categoryAddRequestDto)
    {
        await _categoryService.AddAsync(categoryAddRequestDto);
        return RedirectToAction("Index", "Categories");
    }

    [HttpPost]
    public async Task<IActionResult> Update(CategoryUpdateViewModel viewModel)
    {
        CategoryUpdateRequestDto dto = new()
        {
            Id = viewModel.Id,
            Name = viewModel.Name
        };

        await _categoryService.UpdateAsync(dto);
        return RedirectToAction("Index", "Categories");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var category = await _categoryService.GetByIdForUpdateAsync(id);

        if (category == null)
        {
            return NotFound(); 
        }

        CategoryUpdateViewModel viewModel = new()
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(viewModel);
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);
        return RedirectToAction("Index", "Categories");
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

}