using SmartVenue.Domain.Common;
using SmartVenue.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities
{

    public class Venue : BaseEntity
    {
        public int OwnerId { get; set; }
        public ApplicationUser Owner { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public VenueType VenueType { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int TotalCapacity { get; set; }
        public VenueStatus Status { get; set; } = VenueStatus.Draft;
        public bool IsContracted { get; set; } = true;

        public VenuePolicy? Policy { get; set; }
        public ICollection<VenueImage> Images { get; set; } = new List<VenueImage>();
        public ICollection<VenueOffering> Offerings { get; set; } = new List<VenueOffering>();
        public ICollection<VenueSession> Sessions { get; set; } = new List<VenueSession>();
        public ICollection<SessionOccurrence> Occurrences { get; set; } = new List<SessionOccurrence>();
        public ICollection<VenueEmployee> Employees { get; set; } = new List<VenueEmployee>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<OccupancyRecord> OccupancyRecords { get; set; } = new List<OccupancyRecord>();
    }
}
