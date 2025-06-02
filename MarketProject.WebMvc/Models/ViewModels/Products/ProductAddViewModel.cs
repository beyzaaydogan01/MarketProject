using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MarketProject.WebMvc.Models.ViewModels.Products;

public class ProductAddViewModel
{

    [Required(ErrorMessage = "İsim Alanı Zorunludur.")]
    [MinLength(2, ErrorMessage = "İsim Alanı Minimum 2 haneli olmalıdır.")]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Stock { get; set; }

    public string? Description { get; set; }
    [Required]
    public IFormFile? File { get; set; }

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    public int CategoryId { get; set; }
    public List<SelectListItem> Categories { get; set; } = new();   

}
