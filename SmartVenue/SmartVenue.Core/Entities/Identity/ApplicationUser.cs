
using Microsoft.AspNetCore.Identity;
using smartVenue.core.Common;
using smartVenue.core.Entites.Identity;
using smartVenue.core.Entites.Notifications;
using smartVenue.core.Entites.Ticketing;
using smartVenue.core.Entities.BookingEntites;
using smartVenue.core.Entities.Venues;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.core.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>, IAuditableEntity
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<VenueJoinRequest> VenueJoinRequests { get; set; } = new List<VenueJoinRequest>();
        public ICollection<Venue> Venues { get; set; } = new List<Venue>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<VenueEmployee> VenueEmployments { get; set; } = new List<VenueEmployee>();
    }

}
