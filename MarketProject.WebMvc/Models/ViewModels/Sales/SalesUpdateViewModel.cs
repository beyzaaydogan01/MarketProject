namespace MarketProject.WebMvc.Models.ViewModels.Sales;

public class SalesUpdateViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
