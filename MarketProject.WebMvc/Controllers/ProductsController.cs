using MarketProject.Models.Dtos.Products;
using MarketProject.Service.Abstracts;
using MarketProject.Service.Concretes;
using MarketProject.WebMvc.Models.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;

namespace MarketProject.WebMvc.Controllers;

[Authorize]
public class ProductsController(IProductService productService,
    ICategoryService categoryService) : Controller
{

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var categories = await categoryService.GetAllAsync();

        var viewModel = new ProductAddViewModel
        {
            Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList()
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Add(ProductAddViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            var categories = await categoryService.GetAllAsync();
            vm.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return View(vm);
        }
        
        
        ProductAddRequestDto productAddRequestDto = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            CategoryId = vm.CategoryId,
            Price = vm.Price,
            Stock = vm.Stock,
            File = vm.File
        };

        await productService.AddAsync(productAddRequestDto);

        return RedirectToAction("Index", "Products");
        
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var categories = await categoryService.GetAllAsync();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return View(model);
        }

        var dto = new ProductUpdateRequestDto
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Stock = model.Stock,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            CategoryId= model.CategoryId,
            File = model.File
        };

        await productService.UpdateAsync(dto);

        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var product = await productService.GetByIdForUpdateAsync(id);
        var categories = await categoryService.GetAllAsync();

        var viewModel = new ProductUpdateViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,

            Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = c.Id == product.CategoryId
            }).ToList()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Index()
    {
        var products = await productService.GetAllAsync();

        var productViewModels = products.Select(dto => new ProductListViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            CategoryName = dto.CategoryName,
            Price = dto.Price,
            Stock = dto.Stock,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl
        }).ToList();

        return View(productViewModels);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await productService.DeleteAsync(id);
            TempData["Success"] = "Ürün başarıyla silindi.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        catch (Exception)
        {
            TempData["Error"] = "Silme işlemi sırasında bir hata oluştu.";
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await productService.GetAllByCategoryId(categoryId, CancellationToken.None);
        return Json(products);
    }

}
