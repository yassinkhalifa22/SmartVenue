using System;
using System.Collections.Generic;
using System.Text;

using SmartVenue.Business.Common;
using SmartVenue.Business.Entities.Venues;

namespace SmartVenue.Business.Entities.Ticketing
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
