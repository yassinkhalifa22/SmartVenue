using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Domain.Entities.Venues;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Infrastructure.Persistence.Configurations.VenueManagement
{
    public class SessionTicketTypeConfiguration : IEntityTypeConfiguration<SessionTicketType>
    {
        public void Configure(EntityTypeBuilder<SessionTicketType> b)
        {
            b.ToTable("SessionTicketTypes");
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.Property(x => x.Price).HasPrecision(18, 2);

            b.HasOne(x => x.VenueSession).WithMany(s => s.TicketTypes)
                .HasForeignKey(x => x.VenueSessionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
