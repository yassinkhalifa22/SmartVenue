
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Business.Entities.BookingEntities;

namespace SmartVenue.DataAccess.Configurations
{
    public class BookingItemConfiguration
        : IEntityTypeConfiguration<BookingItem>
    {
        public void Configure(EntityTypeBuilder<BookingItem> builder)
        {
            builder.HasOne(i => i.Booking)
                .WithMany(b => b.Items)
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.SessionTicketType)
                .WithMany(t => t.BookingItems)
                .HasForeignKey(i => i.SessionTicketTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
