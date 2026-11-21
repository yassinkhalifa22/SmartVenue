using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations
{

    public class BookingItemConfiguration : IEntityTypeConfiguration<BookingItem>
    {
        public void Configure(EntityTypeBuilder<BookingItem> b)
        {
            b.ToTable("BookingItems");
            b.Property(x => x.UnitPrice).HasPrecision(18, 2);
            b.Property(x => x.LineTotal).HasPrecision(18, 2);

            b.HasOne(x => x.Booking).WithMany(bk => bk.Items)
                .HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SessionTicketType).WithMany(t => t.BookingItems)
                .HasForeignKey(x => x.SessionTicketTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
