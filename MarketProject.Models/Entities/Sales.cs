using Core.Entities;

namespace MarketProject.Models.Entities;

public class Sales : Entity<int>
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime SalesDate { get; set; } = DateTime.Now;
}
