using SmartVenue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using SmartVenue.Domain.Enums;
using SmartVenue.Domain.Entities.Venues;

namespace SmartVenue.Domain.Entities.Identity
{

    public class VenueJoinRequest : BaseEntity
    {
        public int RequesterId { get; set; }
        public ApplicationUser Requester { get; set; } = null!;

        public string VenueName { get; set; } = string.Empty;
        public VenueType VenueType { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int ApproxCapacity { get; set; }
        public string? Description { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }

        public VenueJoinRequestStatus Status { get; set; } = VenueJoinRequestStatus.PendingReview;
        public int? ReviewedByUserId { get; set; }
        public string? ReviewNote { get; set; }
        public DateTime? ReviewedAt { get; set; }

        public int? CreatedVenueId { get; set; }
        public Venue? CreatedVenue { get; set; }
    }
}
