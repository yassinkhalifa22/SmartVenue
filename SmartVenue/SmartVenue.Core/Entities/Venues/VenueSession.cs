using smartVenue.core.Common;
using smartVenue.core.Entites.BookingEntites;
using smartVenue.core.Entites.Ticketing;
using smartVenue.core.Entities.BookingEntites;
using smartVenue.core.Entities.Identity;
using smartVenue.core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.core.Entities.Venues
{
    public class VenueSession : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int VenueOfferingId { get; set; }
        public VenueOffering VenueOffering { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public WeekDays DaysOfWeek { get; set; } = WeekDays.All;
        public int DefaultCapacity { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<SessionTicketType> TicketTypes { get; set; } = new List<SessionTicketType>();
        public ICollection<SessionOccurrence> Occurrences { get; set; } = new List<SessionOccurrence>();
    }
}
