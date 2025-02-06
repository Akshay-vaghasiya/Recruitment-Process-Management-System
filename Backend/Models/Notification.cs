using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Notification
{
    public int PkNotificationId { get; set; }

    public int? FkUserId { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? FkUser { get; set; }
}
