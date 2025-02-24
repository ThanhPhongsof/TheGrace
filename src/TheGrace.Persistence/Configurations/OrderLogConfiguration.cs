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

public class OrderLogConfiguration : IEntityTypeConfiguration<OrderLog>
{
    public void Configure(EntityTypeBuilder<OrderLog> builder)
    {
        builder.ToTable(TableNames.OrderLogs);

        builder.HasKey(t => t.Id);

        builder.Property(c => c.Status);
        builder.Property(c => c.StatusNote);
        builder.Property(c => c.CreatedAt);
        builder.Property(c => c.CreatedBy);

        builder
            .HasOne(c => c.Order)
            .WithMany(c => c.OrderLogs)
            .HasForeignKey(c => c.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
