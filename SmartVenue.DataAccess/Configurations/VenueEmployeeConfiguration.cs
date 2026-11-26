using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartVenue.Business.Entities.Identity;

namespace SmartVenue.DataAccess.Configurations
{
    public class VenueEmployeeConfiguration
        : IEntityTypeConfiguration<VenueEmployee>
    {
        public void Configure(EntityTypeBuilder<VenueEmployee> builder)
        {
            builder.HasOne(e => e.Venue)
                .WithMany(v => v.Employees)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
