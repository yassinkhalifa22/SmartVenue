using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities
{
    class Booking : BaseEntity
    {
        public string BookingCode { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public ApplicationUser Customer { get; set; } = null!;

        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int VenueOfferingId { get; set; }
        public VenueOffering VenueOffering { get; set; } = null!;

        public int SessionOccurrenceId { get; set; }
        public SessionOccurrence SessionOccurrence { get; set; } = null!;

        public int TotalPeople { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        // Not used in V1 (bookings confirm immediately). Reserved for the payment hold in V2/V3.
        public DateTime? HoldExpiresAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public ICollection<BookingItem> Items { get; set; } = new List<BookingItem>();
        public ICollection<BookingStatusHistory> StatusHistory { get; set; } = new List<BookingStatusHistory>();
        public QrTicket? QrTicket { get; set; }
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
