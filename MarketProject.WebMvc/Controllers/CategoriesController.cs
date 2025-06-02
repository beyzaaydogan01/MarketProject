using MarketProject.Models.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using MarketProject.Service.Abstracts;
using MarketProject.Service.Concretes;
using Microsoft.AspNetCore.Authorization;
using MarketProject.WebMvc.Models.ViewModels.Categories;

namespace MarketProject.WebMvc.Controllers;

[Authorize]
public sealed class CategoriesController(ICategoryService categoryService) : Controller
{

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CategoryAddRequestDto categoryAddRequestDto)
    {
        await categoryService.AddAsync(categoryAddRequestDto);
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

        await categoryService.UpdateAsync(dto);
        return RedirectToAction("Index", "Categories");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var category = await categoryService.GetByIdForUpdateAsync(id);

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


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await categoryService.DeleteAsync(id);
        return RedirectToAction("Index", "Categories");
    }

    public async Task<IActionResult> Index()
    {
        var categories = await categoryService.GetAllAsync();

        var viewModels = categories.Select(dto => new CategoryListViewModel
        {
            Id = dto.Id,
            Name = dto.Name
        }).ToList();

        return View(viewModels);
    }

}