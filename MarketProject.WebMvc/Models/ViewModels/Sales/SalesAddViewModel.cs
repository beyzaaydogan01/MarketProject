using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MarketProject.WebMvc.Models.ViewModels.Sales;

public class SalesAddViewModel
{
    [Required]
    public int? ProductId { get; set; }
    [Required]
    public int? CategoryId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public List<SelectListItem> Products { get; set; } = new();
    public List<SelectListItem> Categories { get; set; } = new();
}
