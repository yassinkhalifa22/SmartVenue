using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using smartVenue.core.Common;
using smartVenue.core.Entites.BookingEntites;
using smartVenue.core.Entites.Identity;
using smartVenue.core.Entites.Notifications;
using smartVenue.core.Entites.Ticketing;
using smartVenue.core.Entities.BookingEntites;
using smartVenue.core.Entities.Identity;
using smartVenue.core.Entities.Venues;
using smartVenue.core.Interfaces;
using smartVenue.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace smartVenue.DataAccess.Context
{
    public class SmartVenueDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> , IAppDbContext
    {
        private readonly ICurrentUserService? _currentUser;

        public SmartVenueDbContext(DbContextOptions<SmartVenueDbContext> options,ICurrentUserService? currentUser = null) : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<VenueJoinRequest> VenueJoinRequests => Set<VenueJoinRequest>();
        public DbSet<Venue> Venues => Set<Venue>();
        public DbSet<VenueImage> VenueImages => Set<VenueImage>();
        public DbSet<VenueOffering> VenueOfferings => Set<VenueOffering>();
        public DbSet<VenuePolicy> VenuePolicies => Set<VenuePolicy>();
        public DbSet<VenueSession> VenueSessions => Set<VenueSession>();
        public DbSet<SessionTicketType> SessionTicketTypes => Set<SessionTicketType>();
        public DbSet<SessionOccurrence> SessionOccurrences => Set<SessionOccurrence>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingItem> BookingItems => Set<BookingItem>();
        public DbSet<BookingStatusHistory> BookingStatusHistories => Set<BookingStatusHistory>();
        public DbSet<QrTicket> QrTickets => Set<QrTicket>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<OccupancyRecord> OccupancyRecords => Set<OccupancyRecord>();
        public DbSet<VenueEmployee> VenueEmployees => Set<VenueEmployee>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(SmartVenueDbContext).Assembly);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        // Fills CreatedAt/CreatedBy/UpdatedAt/UpdatedBy automatically for every entity.
        private void ApplyAuditInfo()
        {
            var now = DateTime.UtcNow;
            var userId = _currentUser?.UserId;

            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.CreatedAt).IsModified = false;
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = userId;
                }
            }
        }
    }
}
