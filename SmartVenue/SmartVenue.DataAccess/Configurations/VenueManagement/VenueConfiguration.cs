using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entities.Venues;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.VenueManagement
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> b)
        {
            b.ToTable("Venues");
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Description).HasMaxLength(2000);
            b.Property(x => x.Address).IsRequired().HasMaxLength(300);
            b.Property(x => x.City).IsRequired().HasMaxLength(100);

            b.HasOne(x => x.Owner).WithMany(u => u.Venues)
                .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.Status, x.VenueType });
            b.HasIndex(x => x.City);
        }
    }
}
