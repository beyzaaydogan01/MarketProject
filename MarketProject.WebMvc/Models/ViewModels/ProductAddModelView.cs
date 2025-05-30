using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MarketProject.WebMvc.Models.ViewModels;

public class ProductAddModelView
{

    [Required(ErrorMessage = "İsim Alanı Zorunludur.")]
    [MinLength(3, ErrorMessage = "İsim Alanı Minimum 3 haneli olmalıdır.")]
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Description { get; set; }

    public int CategoryId { get; set; }

}