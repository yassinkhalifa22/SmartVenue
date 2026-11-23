using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartVenue.core.Entites.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.DataAccess.Configurations.Notifications
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> b)
        {
            b.ToTable("Notifications");
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.Message).IsRequired().HasMaxLength(1000);
            b.Property(x => x.RelatedEntityType).HasMaxLength(100);

            b.HasOne(x => x.User).WithMany(u => u.Notifications)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.UserId, x.IsRead });
        }
    }

}
