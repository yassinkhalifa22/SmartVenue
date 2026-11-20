using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Enums
{
    public enum VenueJoinRequestStatus
    {
        PendingReview = 0,
        UnderReview = 1,
        Approved = 2,
        Rejected = 3,
        NeedsMoreInformation = 4
    }

}
