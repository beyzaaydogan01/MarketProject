using MarketProject.Models.Dtos.Products;
using MarketProject.Service.Abstracts;
using MarketProject.Service.Concretes;
using MarketProject.WebMvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketProject.WebMvc.Controllers;

[Authorize]
public class ProductsController(IProductService productService) : Controller
{

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(ProductAddModelView vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        ProductAddRequestDto productAddRequestDto = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            CategoryId = vm.CategoryId,
            Price = vm.Price,
            Stock = vm.Stock
        };

        await productService.AddAsync(productAddRequestDto);

        return RedirectToAction("Index", "Products");
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductUpdateViewModel model)
    {

        var dto = new ProductUpdateRequestDto
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Stock = model.Stock,
            Description = model.Description,
            ImageUrl = model.ImageUrl
        };

        await productService.UpdateAsync(dto);

        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var product = await productService.GetByIdForUpdateAsync(id);

        var viewModel = new ProductUpdateViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            ImageUrl = product.ImageUrl
        };

        return View(viewModel);
    }


    public async Task<IActionResult> Index()
    {
        var products = await productService.GetAllAsync();
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await productService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
