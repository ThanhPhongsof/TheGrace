using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheGrace.Domain.Entities;
using TheGrace.Domain.Enumerations;
using TheGrace.Persistence.Constants;

namespace TheGrace.Persistence.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable(TableNames.ProductCategories);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Type).IsRequired().HasDefaultValue(StatusEnum.Active);
        builder.Property(c => c.Name).HasDefaultValue(string.Empty);
    }
}
