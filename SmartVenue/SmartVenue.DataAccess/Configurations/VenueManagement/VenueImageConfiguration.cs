using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entities.Venues;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.VenueManagement
{

    public class VenueImageConfiguration : IEntityTypeConfiguration<VenueImage>
    {
        public void Configure(EntityTypeBuilder<VenueImage> b)
        {
            b.ToTable("VenueImages");
            b.Property(x => x.Url).IsRequired().HasMaxLength(500);

            b.HasOne(x => x.Venue).WithMany(v => v.Images)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
