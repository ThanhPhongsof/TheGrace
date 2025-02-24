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

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Orders);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Code);
        builder.Property(c => c.Status);
        builder.Property(c => c.OrderDate);
        builder.Property(c => c.CreatedDate);
        builder.Property(c => c.StatusNote);
        builder.Property(c => c.IsPayment);
        builder.Property(c => c.ShipType);
        builder.Property(c => c.ShipPrice);
        builder.Property(c => c.DiscountType);
        builder.Property(c => c.Discount);
        builder.Property(c => c.DiscountPercent);
        builder.Property(c => c.TotalDiscount);
        builder.Property(c => c.TotalQuantity);
        builder.Property(c => c.TotalPrice);
        builder.Property(c => c.TotalPayment);
        builder.Property(c => c.TotalPoint);
        builder.Property(c => c.CompletedDate);
        builder.Property(c => c.Note);
        builder.Property(c => c.ItemProducts);

        builder
            .HasOne(c => c.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(c => c.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.DeliveryPartner)
            .WithMany(c => c.Orders)
            .HasForeignKey(c => c.DeliveryPartnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
