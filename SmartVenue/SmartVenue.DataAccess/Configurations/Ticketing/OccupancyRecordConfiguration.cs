using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entites.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.Ticketing
{
    public class OccupancyRecordConfiguration : IEntityTypeConfiguration<OccupancyRecord>
    {
        public void Configure(EntityTypeBuilder<OccupancyRecord> b)
        {
            b.ToTable("OccupancyRecords");

            b.HasOne(x => x.Venue).WithMany(v => v.OccupancyRecords)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<Visit>().WithMany()
                .HasForeignKey(x => x.VisitId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.VenueId, x.RecordedAt });
        }
    }
}
