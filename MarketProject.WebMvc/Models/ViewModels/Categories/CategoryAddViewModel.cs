using System.ComponentModel.DataAnnotations;

namespace MarketProject.WebMvc.Models.ViewModels.Categories;

public class CategoryAddViewModel
{
    [Required]
    public string Name { get; set; }
}