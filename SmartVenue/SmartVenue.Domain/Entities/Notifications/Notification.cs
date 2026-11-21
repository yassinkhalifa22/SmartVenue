using SmartVenue.Domain.Common;
using SmartVenue.Domain.Entities.Identity;
using SmartVenue.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Domain.Entities.Notifications
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public NotificationType Type { get; set; } = NotificationType.General;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public bool IsRead { get; set; }
    }
}
