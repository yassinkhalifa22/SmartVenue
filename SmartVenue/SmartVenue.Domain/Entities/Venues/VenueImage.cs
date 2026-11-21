using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities.Venues
{

    public class VenueImage : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public string Url { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
    }
}
