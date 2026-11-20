using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Application.Common
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }
}
