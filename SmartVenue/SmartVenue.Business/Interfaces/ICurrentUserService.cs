using System;
using System.Collections.Generic;
using System.Text;


namespace SmartVenue.Business.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }
}

