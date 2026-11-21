using SmartVenue.Domain.Common;
using SmartVenue.Domain.Entities.Identity;
using SmartVenue.Domain.Entities.Venues;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities.Ticketing
{

    public class VenueEmployee : BaseEntity
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
