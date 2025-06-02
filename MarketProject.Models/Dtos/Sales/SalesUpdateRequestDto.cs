
namespace MarketProject.Models.Dtos.Sales;

public class SalesUpdateRequestDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
