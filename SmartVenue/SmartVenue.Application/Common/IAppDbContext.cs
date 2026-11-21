 using SmartVenue.Domain.Entities.BookingEntites;
 using SmartVenue.Domain.Entities.Identity;
 using SmartVenue.Domain.Entities.Notifications;
 using SmartVenue.Domain.Entities.Ticketing;
 using SmartVenue.Domain.Entities.Venues;
 using Microsoft.EntityFrameworkCore;
 using SmartVenue.Domain.Entities;

namespace SmartVenue.Application.Common.Interfaces
{ 
    public interface IAppDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<BookingItem> BookingItems { get; }
        DbSet<BookingStatusHistory> BookingStatusHistories { get; }
        DbSet<VenueJoinRequest> VenueJoinRequests { get; }
        DbSet<OccupancyRecord> OccupancyRecords { get; }
        DbSet<QrTicket> QrTickets { get; }
        DbSet<VenueEmployee> VenueEmployees { get; }
        DbSet<Visit> Visits { get; }
        DbSet<SessionOccurrence> SessionOccurrences { get; }
        DbSet<SessionTicketType> SessionTicketTypes { get; }
        DbSet<Venue> Venues { get; }
        DbSet<VenueImage> VenueImages { get; }
        DbSet<VenueOffering> VenueOfferings { get; }
        DbSet<VenuePolicy> VenuePolicies { get; }
        DbSet<VenueSession> VenueSessions { get; }
        DbSet<Notification> Notifications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
