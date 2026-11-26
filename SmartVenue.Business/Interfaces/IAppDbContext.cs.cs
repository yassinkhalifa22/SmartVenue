using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SmartVenue.Business.Entities.BookingEntities;
using SmartVenue.Business.Entities.Identity;
using SmartVenue.Business.Entities.Notifications;
using SmartVenue.Business.Entities.Ticketing;
using SmartVenue.Business.Entities.Venues;

namespace SmartVenue.Business.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; }
        DbSet<VenueJoinRequest> VenueJoinRequests { get; }

        DbSet<Venue> Venues { get; }
        DbSet<VenueImage> VenueImages { get; }
        DbSet<VenueOffering> VenueOfferings { get; }
        DbSet<VenuePolicy> VenuePolicies { get; }
        DbSet<VenueSession> VenueSessions { get; }
        DbSet<SessionTicketType> SessionTicketTypes { get; }
        DbSet<SessionOccurrence> SessionOccurrences { get; }

        DbSet<Booking> Bookings { get; }
        DbSet<BookingItem> BookingItems { get; }
        DbSet<BookingStatusHistory> BookingStatusHistories { get; }

        DbSet<QrTicket> QrTickets { get; }
        DbSet<Visit> Visits { get; }
        DbSet<OccupancyRecord> OccupancyRecords { get; }
        DbSet<VenueEmployee> VenueEmployees { get; }

        DbSet<Notification> Notifications { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}