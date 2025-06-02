using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MarketProject.WebMvc.Models.ViewModels.Products;

public class ProductUpdateViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public List<SelectListItem> Categories { get; set; } = new();

    public IFormFile? File { get; set; }
}
