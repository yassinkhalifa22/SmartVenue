using System;
using System.Collections.Generic;
using System.Text;

namespace smartVenue.core.Entites;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
