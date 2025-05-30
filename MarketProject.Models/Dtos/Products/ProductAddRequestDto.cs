﻿
namespace MarketProject.Models.Dtos.Products;

public sealed class ProductAddRequestDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public int CategoryId { get; set; }
}