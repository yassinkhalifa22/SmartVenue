using SmartVenue.Domain.Common;
using SmartVenue.Domain.Entities.Venues;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities.BookingEntites
{

    public class BookingItem : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public int SessionTicketTypeId { get; set; }
        public SessionTicketType SessionTicketType { get; set; } = null!;

        public int Quantity { get; set; }

        // Price snapshot at booking time.
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

}
