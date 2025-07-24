using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class ActivitySchedule
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string ActivityName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<ActivitySession> ActivitySessions { get; set; } = new List<ActivitySession>();

    public virtual ICollection<ReminderNotification> ReminderNotifications { get; set; } = new List<ReminderNotification>();

    public virtual User User { get; set; } = null!;
}
