using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.core.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }
}
