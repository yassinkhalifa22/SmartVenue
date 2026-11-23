using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entities.BookingEntites;
using smartVenue.core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.Bookingconf
{
    public class BookingStatusHistoryConfiguration : IEntityTypeConfiguration<BookingStatusHistory>
    {
        public void Configure(EntityTypeBuilder<BookingStatusHistory> b)
        {
            b.ToTable("BookingStatusHistories");
            b.Property(x => x.Reason).HasMaxLength(500);

            b.HasOne(x => x.Booking).WithMany(bk => bk.StatusHistory)
                .HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(x => x.ChangedByUserId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.BookingId, x.ChangedAt });
        }
    }
}
