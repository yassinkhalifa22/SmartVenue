using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entites.Identity;
using smartVenue.core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.Identity;
    public class VenueJoinRequestConfiguration : IEntityTypeConfiguration<VenueJoinRequest>
    {
        public void Configure(EntityTypeBuilder<VenueJoinRequest> b)
        {
            b.ToTable("VenueJoinRequests");
            b.Property(x => x.VenueName).IsRequired().HasMaxLength(200);
            b.Property(x => x.Address).IsRequired().HasMaxLength(300);
            b.Property(x => x.City).IsRequired().HasMaxLength(100);
            b.Property(x => x.Description).HasMaxLength(2000);
            b.Property(x => x.ContactPhone).IsRequired().HasMaxLength(30);
            b.Property(x => x.WebsiteUrl).HasMaxLength(500);
            b.Property(x => x.ReviewNote).HasMaxLength(1000);

            b.HasOne(x => x.Requester).WithMany(u => u.VenueJoinRequests)
                .HasForeignKey(x => x.RequesterId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(x => x.ReviewedByUserId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.CreatedVenue).WithMany()
                .HasForeignKey(x => x.CreatedVenueId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.Status);
        }
    }


