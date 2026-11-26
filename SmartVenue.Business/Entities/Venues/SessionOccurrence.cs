using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations.Schema;
using SmartVenue.Business.Common;
using SmartVenue.Business.Entities.BookingEntities;
using SmartVenue.Business.Entities.Ticketing;
using SmartVenue.Business.Enums;

namespace SmartVenue.Business.Entities.Venues
{
    public class SessionOccurrence : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int VenueSessionId { get; set; }
        public VenueSession VenueSession { get; set; } = null!;

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public int Capacity { get; set; }

        // Written only by BookingService inside a transaction.
        public int ReservedCount { get; set; }

        public SessionOccurrenceStatus Status { get; set; }
            = SessionOccurrenceStatus.Open;

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        [NotMapped]
        public int AvailableCapacity => Capacity - ReservedCount;

        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();

        public ICollection<Visit> Visits { get; set; }
            = new List<Visit>();
    }
}
