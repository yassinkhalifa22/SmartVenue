using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
{

    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> b)
        {
            b.ToTable("Visits");

            b.HasOne(x => x.QrTicket).WithMany(q => q.Visits)
                .HasForeignKey(x => x.QrTicketId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Booking).WithMany(bk => bk.Visits)
                .HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Venue).WithMany(v => v.Visits)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SessionOccurrence).WithMany(o => o.Visits)
                .HasForeignKey(x => x.SessionOccurrenceId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(x => x.CheckedInByUserId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(x => x.CheckedOutByUserId).OnDelete(DeleteBehavior.Restrict);

            // Only one open (InProgress = 0) visit per booking.
            b.HasIndex(x => x.BookingId).IsUnique().HasFilter("[Status] = 0");
            b.HasIndex(x => new { x.VenueId, x.Status });
        }
    }
}
