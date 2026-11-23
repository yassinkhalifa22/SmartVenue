
using smartVenue.core.Common;
using smartVenue.core.Entities.Venues;

using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.core.Entites.Ticketing
{
    public class OccupancyRecord : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int OccupancyCount { get; set; }
        public int Delta { get; set; }
        public int? VisitId { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
