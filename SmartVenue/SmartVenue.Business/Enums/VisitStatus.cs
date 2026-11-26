using System;
using System.Collections.Generic;
using System.Text;


namespace SmartVenue.Business.Enums
{
    public enum VisitStatus
    {
        // InProgress must stay = 0.
        // The filtered unique index on Visit uses "[Status] = 0".
        InProgress = 0,
        Completed = 1
    }
}
