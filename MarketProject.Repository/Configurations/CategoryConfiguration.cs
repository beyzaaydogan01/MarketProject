
using MarketProject.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MarketProject.Repository.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).HasColumnName("Created").IsRequired();
        builder.Property(x => x.UpdatedDate).HasColumnName("Updated");
        builder.Property(x => x.Name).HasColumnName("Category_Name");

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);

        builder.Navigation(x => x.Products).AutoInclude();
    }
}