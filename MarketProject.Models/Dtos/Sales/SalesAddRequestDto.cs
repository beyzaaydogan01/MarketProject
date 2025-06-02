
namespace MarketProject.Models.Dtos.Sales;

public class SalesAddRequestDto
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
