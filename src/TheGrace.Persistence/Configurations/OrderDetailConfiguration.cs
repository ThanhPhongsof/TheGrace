using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheGrace.Domain.Entities;
using TheGrace.Persistence.Constants;

namespace TheGrace.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable(TableNames.OrderDetails);

        builder.HasKey(t => t.Id);

        builder.Property(c => c.Quantity);
        builder.Property(c => c.Discount);
        builder.Property(c => c.Price);
        builder.Property(c => c.TotalPrice);

        builder
            .HasOne(c => c.Order)
            .WithMany(c => c.OrderDetails)
            .HasForeignKey(c => c.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
