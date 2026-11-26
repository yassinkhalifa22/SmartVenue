using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Business.Entities.Venues;

namespace SmartVenue.DataAccess.Configurations
{
    public class VenueSessionConfiguration
        : IEntityTypeConfiguration<VenueSession>
    {
        public void Configure(EntityTypeBuilder<VenueSession> builder)
        {
            builder.HasOne(s => s.Venue)
                .WithMany(v => v.Sessions)
                .HasForeignKey(s => s.VenueId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

