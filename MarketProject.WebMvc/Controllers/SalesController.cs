using MarketProject.Models.Dtos.Products;
using MarketProject.Models.Dtos.Sales;
using MarketProject.WebMvc.Models.ViewModels.Sales;
using MarketProject.Service.Abstracts;
using MarketProject.Service.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MarketProject.WebMvc.Controllers;

[Authorize]
public class SalesController(ISalesService salesService,
    IProductService productService,
    ICategoryService categoryService
    ) : Controller
{

    public async Task<IActionResult> Index()
    {
        var salesList = await salesService.GetAllAsync();

        var salesViewModelList = salesList.Select(dto => new SalesListViewModel
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            CategoryName = dto.CategoryName,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice,
            TotalPrice = dto.TotalPrice,
            SalesDate = dto.SalesDate
        }).ToList();

        return View(salesViewModelList);
    }

    [HttpGet]
    public async Task<IActionResult> AddAsync()
    {
        var products = await productService.GetAllAsync();
        var categories = await categoryService.GetAllAsync();

        var viewModel = new SalesAddViewModel
        {
            Products = products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList(),

            Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList()
        };

        return View(viewModel); ;
    }

    [HttpPost]
    public async Task<IActionResult> Add(SalesAddViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            await SetViewBagsAsync(vm);
            return View(vm);
        }

        SalesAddRequestDto dto = new()
        {
            ProductId = (int)vm.ProductId,
            CategoryId = (int)vm.CategoryId,
            Quantity = vm.Quantity,
            UnitPrice = vm.UnitPrice
        };

        try
        {
            await salesService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            await SetViewBagsAsync(vm);
            return View(vm);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var sales = await salesService.GetByIdForUpdateAsync(id);

        var vm = new SalesUpdateViewModel
        {
            Id = sales.Id,
            ProductId = sales.ProductId,
            CategoryId = sales.CategoryId,
            Quantity = sales.Quantity,
            UnitPrice = sales.UnitPrice
        };

        var categories = await categoryService.GetAllAsync();
        var products = await productService.GetAllAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name", sales.CategoryId);
        ViewBag.Products = new SelectList(products, "Id", "Name", sales.ProductId);

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(SalesUpdateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            var categories = await categoryService.GetAllAsync();
            var products = await productService.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name", vm.CategoryId);
            ViewBag.Products = new SelectList(products, "Id", "Name", vm.ProductId);

            return View(vm);
        }

        var updateDto = new SalesUpdateRequestDto
        {
            Id = vm.Id,
            ProductId = vm.ProductId,
            CategoryId = vm.CategoryId,
            Quantity = vm.Quantity,
            UnitPrice = vm.UnitPrice
        };

        await salesService.UpdateAsync(updateDto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await salesService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await productService.GetAllByCategoryId(categoryId);

        var result = products.Select(p => new
        {
            id = p.Id,
            name = p.Name
        });

        return Json(result);
    }

    private async Task SetViewBagsAsync(SalesAddViewModel vm)
    {
        var products = await productService.GetAllAsync();
        var categories = await categoryService.GetAllAsync();

        vm.Products = products.Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToList();
        vm.Categories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
    }
}
