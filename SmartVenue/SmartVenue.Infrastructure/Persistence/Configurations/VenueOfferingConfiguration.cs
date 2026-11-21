using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
{
    public class VenueOfferingConfiguration : IEntityTypeConfiguration<VenueOffering>
    {
        public void Configure(EntityTypeBuilder<VenueOffering> b)
        {
            b.ToTable("VenueOfferings");
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Description).HasMaxLength(1000);
            b.Property(x => x.BasePrice).HasPrecision(18, 2);

            b.HasOne(x => x.Venue).WithMany(v => v.Offerings)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
        }
    }

}
