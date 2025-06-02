
using MarketProject.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MarketProject.Repository.Configurations;

public sealed class SalesConfiguration : IEntityTypeConfiguration<Sales>
{
    public void Configure(EntityTypeBuilder<Sales> builder)
    {
        builder.ToTable("Sales").HasKey(x => x.Id);
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).HasColumnName("Unit_Price")
            .HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.TotalPrice).HasColumnName("Total_Price")
            .HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.SalesDate).HasColumnName("SalesDate")
            .HasColumnType("datetime").IsRequired();

        builder.HasOne(x => x.Product)
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
               .WithMany()
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}