using smartVenue.core.Common;
using smartVenue.core.Entites.BookingEntites;
using smartVenue.core.Entites.Ticketing;
using smartVenue.core.Entities.BookingEntites;
using smartVenue.core.Entities.Identity;
using smartVenue.core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.core.Entities.Venues
{

    public class VenueOffering : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<VenueSession> Sessions { get; set; } = new List<VenueSession>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
