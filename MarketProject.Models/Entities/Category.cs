using Core.Entities;

namespace MarketProject.Models.Entities;

public sealed class Category : Entity<int>
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
