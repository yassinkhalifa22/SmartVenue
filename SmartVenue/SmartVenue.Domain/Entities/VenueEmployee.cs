using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities
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
