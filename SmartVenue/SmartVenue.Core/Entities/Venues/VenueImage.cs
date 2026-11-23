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

    public class VenueImage : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public string Url { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
    }
}
