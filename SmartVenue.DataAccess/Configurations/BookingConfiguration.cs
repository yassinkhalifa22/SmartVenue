
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Business.Entities.BookingEntities;

namespace SmartVenue.DataAccess.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.RowVersion)
                .IsRowVersion();

            builder.HasOne(b => b.Customer)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Venue)
                .WithMany(v => v.Bookings)
                .HasForeignKey(b => b.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.VenueOffering)
                .WithMany(o => o.Bookings)
                .HasForeignKey(b => b.VenueOfferingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.SessionOccurrence)
                .WithMany(o => o.Bookings)
                .HasForeignKey(b => b.SessionOccurrenceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
