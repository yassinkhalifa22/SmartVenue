
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Business.Entities.Ticketing;

namespace SmartVenue.DataAccess.Configurations
{
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasOne(v => v.Booking)
                .WithMany(b => b.Visits)
                .HasForeignKey(v => v.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.QrTicket)
                .WithMany(q => q.Visits)
                .HasForeignKey(v => v.QrTicketId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(v => v.SessionOccurrence)
                .WithMany(o => o.Visits)
                .HasForeignKey(v => v.SessionOccurrenceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(v => v.Venue)
                .WithMany(vn => vn.Visits)
                .HasForeignKey(v => v.VenueId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
