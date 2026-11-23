 using smartVenue.core.Entities.BookingEntites;
 using smartVenue.core.Entities.Identity;
 using smartVenue.core.Entities;
 using smartVenue.core.Entities.Venues;
 using Microsoft.EntityFrameworkCore;
using smartVenue.core.Entites.BookingEntites;
using smartVenue.core.Entites.Identity;
using smartVenue.core.Entites.Ticketing;
using smartVenue.core.Entites.Notifications;

namespace smartVenue.core.Interfaces
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
