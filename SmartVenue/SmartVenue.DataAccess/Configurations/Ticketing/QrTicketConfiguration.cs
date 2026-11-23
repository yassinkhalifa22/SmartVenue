using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entites.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.Ticketing
{
    public class QrTicketConfiguration : IEntityTypeConfiguration<QrTicket>
    {
        public void Configure(EntityTypeBuilder<QrTicket> b)
        {
            b.ToTable("QrTickets");
            b.Property(x => x.TicketCodeHash).IsRequired().HasMaxLength(128);

            b.HasOne(x => x.Booking).WithOne(bk => bk.QrTicket)
                .HasForeignKey<QrTicket>(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.BookingId).IsUnique();
            b.HasIndex(x => x.TicketCodeHash).IsUnique();
        }
    }
}
