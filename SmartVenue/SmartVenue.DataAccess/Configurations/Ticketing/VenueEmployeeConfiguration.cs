using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entites.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.Ticketing
{
    public class VenueEmployeeConfiguration : IEntityTypeConfiguration<VenueEmployee>
    {
        public void Configure(EntityTypeBuilder<VenueEmployee> b)
        {
            b.ToTable("VenueEmployees");

            b.HasOne(x => x.User).WithMany(u => u.VenueEmployments)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Venue).WithMany(v => v.Employees)
                .HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.UserId, x.VenueId }).IsUnique();
        }
    }
}
