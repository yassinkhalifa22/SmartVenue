using SmartVenue.Business.Common;
using SmartVenue.Business.Entities.Venues;

namespace SmartVenue.Business.Entities.Identity
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