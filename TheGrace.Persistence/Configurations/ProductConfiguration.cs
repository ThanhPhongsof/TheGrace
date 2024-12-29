using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheGrace.Domain.Entities;
using TheGrace.Domain.Enumerations;
using TheGrace.Persistence.Constants;

namespace TheGrace.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Products);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Type).IsRequired().HasDefaultValue(StatusEnum.Active);
        builder.Property(c => c.Name).HasDefaultValue(string.Empty);
        builder.Property(c => c.Image).HasDefaultValue(string.Empty);
        builder.Property(c => c.Description).HasDefaultValue(string.Empty);
        builder.Property(c => c.Code).HasDefaultValue(string.Empty);
        builder.Property(c => c.Price).HasDefaultValue(1000000);
        builder.Property(c => c.Quantity).HasDefaultValue(1000);

        builder
            .HasOne(c => c.ProductCategory)
            .WithMany(c => c.Products)
            .HasForeignKey(c => c.ProductCategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
