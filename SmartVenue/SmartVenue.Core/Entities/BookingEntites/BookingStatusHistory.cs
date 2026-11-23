using smartVenue.core.Common;
using smartVenue.core.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.core.Entities.BookingEntites
{

    public class BookingStatusHistory : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public BookingStatus? FromStatus { get; set; }
        public BookingStatus ToStatus { get; set; }
        public int? ChangedByUserId { get; set; }
        public string? Reason { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
