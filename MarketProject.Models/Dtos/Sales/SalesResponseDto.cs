using MarketProject.Models.Entities;

namespace MarketProject.Models.Dtos.Sales;

public class SalesResponseDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime SalesDate { get; set; }
}
