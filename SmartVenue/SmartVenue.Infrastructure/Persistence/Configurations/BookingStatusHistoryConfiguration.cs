using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
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
