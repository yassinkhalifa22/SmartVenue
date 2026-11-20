using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities
{

    public class VenueOffering : BaseEntity
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<VenueSession> Sessions { get; set; } = new List<VenueSession>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
