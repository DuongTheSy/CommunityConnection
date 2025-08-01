using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class ActivitySchedule
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string ActivityName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<ActivitySession> ActivitySessions { get; set; } = new List<ActivitySession>();

    public virtual ICollection<ReminderNotification> ReminderNotifications { get; set; } = new List<ReminderNotification>();

    public virtual User User { get; set; } = null!;
}
