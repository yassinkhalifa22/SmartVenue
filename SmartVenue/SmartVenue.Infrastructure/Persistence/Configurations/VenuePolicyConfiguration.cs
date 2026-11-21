using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
{
    public class VenuePolicyConfiguration : IEntityTypeConfiguration<VenuePolicy>
    {
        public void Configure(EntityTypeBuilder<VenuePolicy> b)
        {
            b.ToTable("VenuePolicies");

            b.HasOne(x => x.Venue).WithOne(v => v.Policy)
                .HasForeignKey<VenuePolicy>(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.VenueId).IsUnique();
        }
    }
}
