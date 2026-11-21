using SmartVenue.Domain.Common;
using SmartVenue.Domain.Entities.Ticketing;
using SmartVenue.Domain.Entities.BookingEntites;
using SmartVenue.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartVenue.Domain.Entities.Venues
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

        // هنا يتم الكتابة فقط بواسطة BookingService داخل معاملة.
        public int ReservedCount { get; set; }

        public SessionOccurrenceStatus Status { get; set; } = SessionOccurrenceStatus.Open;

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        [NotMapped]
        public int AvailableCapacity => Capacity - ReservedCount;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
