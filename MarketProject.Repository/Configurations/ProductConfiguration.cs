using MarketProject.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MarketProject.Repository.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).HasColumnName("Created").IsRequired();
        builder.Property(x => x.UpdatedDate).HasColumnName("Updated");
        builder.Property(x => x.Name).HasColumnName("Product_Name");
        builder.Navigation(x => x.Category).AutoInclude();
    }
}