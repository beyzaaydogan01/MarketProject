
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MarketProject.Models.Entities;

namespace MarketProject.Repository.Contexts;

public sealed class BaseDbContext : DbContext
{

    public BaseDbContext(DbContextOptions<BaseDbContext> opt) : base(opt)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB; Database=MarketDb;Trusted_Connection=true");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}