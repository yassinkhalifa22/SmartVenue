using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
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
