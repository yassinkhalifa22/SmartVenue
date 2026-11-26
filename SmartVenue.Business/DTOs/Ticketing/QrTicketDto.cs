using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Business.DTOs.Ticketing
{
    public class QrTicketDto
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        // The original token returned only when the ticket is generated.
        public string Token { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime IssuedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}
