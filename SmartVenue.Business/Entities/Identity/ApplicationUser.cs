using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SmartVenue.Business.Common;
using SmartVenue.Business.Entities.BookingEntities;
using SmartVenue.Business.Entities.Notifications;
using SmartVenue.Business.Entities.Venues;

namespace SmartVenue.Business.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>, IAuditableEntity
    {
        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<VenueJoinRequest> VenueJoinRequests { get; set; }
            = new List<VenueJoinRequest>();

        public ICollection<Venue> Venues { get; set; }
            = new List<Venue>();

        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();

        public ICollection<Notification> Notifications { get; set; }
            = new List<Notification>();

        public ICollection<VenueEmployee> VenueEmployments { get; set; }
            = new List<VenueEmployee>();
    }
}
