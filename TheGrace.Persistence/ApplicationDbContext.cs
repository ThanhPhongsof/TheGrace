using Microsoft.EntityFrameworkCore;
using TheGrace.Domain.Entities;

namespace TheGrace.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

    protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<ProductCategory> ProductCategories { get; set; }

    public DbSet<Product> Products { get; set; }
}
