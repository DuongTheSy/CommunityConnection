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

    public DateTime? ActualStartTime { get; set; }

    public DateTime? ActualEndTime { get; set; }

    public string? Notes { get; set; }

    public virtual ReminderNotification? ReminderNotification { get; set; }

    public virtual User User { get; set; } = null!;
}
