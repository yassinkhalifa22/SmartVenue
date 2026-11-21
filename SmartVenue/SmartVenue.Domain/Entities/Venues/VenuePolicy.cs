using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities.Venues
{
    public class VenuePolicy : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int CancellationCutoffMinutes { get; set; } = 60;
        public int MaxPartySize { get; set; } = 10;
    }
}
