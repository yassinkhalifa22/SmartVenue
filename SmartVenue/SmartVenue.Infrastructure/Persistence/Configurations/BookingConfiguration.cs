using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using SmartVenue.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> b)
        {
            b.ToTable("Bookings");
            b.Property(x => x.BookingCode).IsRequired().HasMaxLength(20);
            b.Property(x => x.TotalAmount).HasPrecision(18, 2);
            b.Property(x => x.RowVersion).IsRowVersion();

            b.HasOne(x => x.Customer).WithMany(u => u.Bookings)
                .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Venue).WithMany(v => v.Bookings)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.VenueOffering).WithMany(o => o.Bookings)
                .HasForeignKey(x => x.VenueOfferingId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SessionOccurrence).WithMany(o => o.Bookings)
                .HasForeignKey(x => x.SessionOccurrenceId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.BookingCode).IsUnique();
            b.HasIndex(x => new { x.CustomerId, x.Status });
            b.HasIndex(x => new { x.SessionOccurrenceId, x.Status });
        }
    }
}
