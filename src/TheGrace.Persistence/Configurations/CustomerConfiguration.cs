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

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(TableNames.Customers);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name);
        builder.Property(c => c.PhonePrimary);
        builder.Property(c => c.PhoneSecond).HasDefaultValue(string.Empty);
        builder.Property(c => c.Address);
    }
}
