using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entities.Venues;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.VenueManagement
{
    public class VenueSessionConfiguration : IEntityTypeConfiguration<VenueSession>
    {
        public void Configure(EntityTypeBuilder<VenueSession> b)
        {
            b.ToTable("VenueSessions");
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);

            b.HasOne(x => x.Venue).WithMany(v => v.Sessions)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.VenueOffering).WithMany(o => o.Sessions)
                .HasForeignKey(x => x.VenueOfferingId).OnDelete(DeleteBehavior.Restrict);
        }
    }

}
