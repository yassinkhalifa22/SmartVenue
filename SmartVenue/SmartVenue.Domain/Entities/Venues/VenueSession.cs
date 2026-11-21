using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using SmartVenue.Domain.Enums;

namespace SmartVenue.Domain.Entities.Venues
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
