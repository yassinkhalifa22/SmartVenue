using smartVenue.core.Common;
using smartVenue.core.Entites.BookingEntites;
using smartVenue.core.Entites.Ticketing;
using smartVenue.core.Entities.BookingEntites;
using smartVenue.core.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace smartVenue.core.Entities.Venues
{

    public class SessionTicketType : BaseEntity
    {
        public int VenueSessionId { get; set; }
        public VenueSession VenueSession { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<BookingItem> BookingItems { get; set; } = new List<BookingItem>();
    }

}
