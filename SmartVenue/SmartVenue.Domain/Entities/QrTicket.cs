using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities
{
    public class QrTicket : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public string TicketCodeHash { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Active;
        public DateTime IssuedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }

}
