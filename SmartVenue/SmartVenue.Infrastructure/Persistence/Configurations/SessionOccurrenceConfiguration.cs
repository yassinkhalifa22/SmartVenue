using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
{
    public class SessionOccurrenceConfiguration : IEntityTypeConfiguration<SessionOccurrence>
    {
        public void Configure(EntityTypeBuilder<SessionOccurrence> b)
        {
            b.ToTable("SessionOccurrences");
            b.Property(x => x.RowVersion).IsRowVersion();
            b.Ignore(x => x.AvailableCapacity);

            b.HasOne(x => x.Venue).WithMany(v => v.Occurrences)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.VenueSession).WithMany(s => s.Occurrences)
                .HasForeignKey(x => x.VenueSessionId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.VenueSessionId, x.StartAt }).IsUnique();
            b.HasIndex(x => new { x.VenueId, x.StartAt });
        }
    }
}
