using SmartVenue.Domain.Common;
using SmartVenue.Domain.Entities.BookingEntites;
using System;
using System.Collections.Generic;
using System.Text;


namespace SmartVenue.Domain.Entities.Venues
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
