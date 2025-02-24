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

public class DeliveryPartnerConfiguration : IEntityTypeConfiguration<DeliveryPartner>
{
    public void Configure(EntityTypeBuilder<DeliveryPartner> builder)
    {
        builder.ToTable(TableNames.DeliveryPartners);

        builder.HasKey(t => t.Id);

        builder.Property(c => c.Name);
        builder.Property(c => c.Price);
    }
}
