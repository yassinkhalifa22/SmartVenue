
using SmartVenue.Business.Common;
using SmartVenue.Business.Entities.BookingEntities;
using SmartVenue.Business.Entities.Venues;
using SmartVenue.Business.Enums;

namespace SmartVenue.Business.Entities.Ticketing
{
    public class Visit : BaseEntity
    {
        public int QrTicketId { get; set; }
        public QrTicket QrTicket { get; set; } = null!;

        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int SessionOccurrenceId { get; set; }
        public SessionOccurrence SessionOccurrence { get; set; } = null!;

        public int PartySize { get; set; }

        public VisitStatus Status { get; set; } = VisitStatus.InProgress;

        public DateTime CheckInAt { get; set; }
        public DateTime? CheckOutAt { get; set; }

        public int? CheckedInByUserId { get; set; }
        public int? CheckedOutByUserId { get; set; }

        public EntryMethod EntryMethod { get; set; } = EntryMethod.QrScan;
        public ExitMethod? ExitMethod { get; set; }
    }
}
