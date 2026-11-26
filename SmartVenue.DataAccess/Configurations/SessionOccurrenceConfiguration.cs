
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Business.Entities.Venues;

namespace SmartVenue.DataAccess.Configurations
{
    public class SessionOccurrenceConfiguration
        : IEntityTypeConfiguration<SessionOccurrence>
    {
        public void Configure(EntityTypeBuilder<SessionOccurrence> builder)
        {
            builder.HasOne(o => o.Venue)
                .WithMany(v => v.Occurrences)
                .HasForeignKey(o => o.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.VenueSession)
                .WithMany(s => s.Occurrences)
                .HasForeignKey(o => o.VenueSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
