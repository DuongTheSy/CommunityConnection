using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class ActivitySession
{
    public long SessionId { get; set; }

    public long ActivityId { get; set; }

    public long UserId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Notes { get; set; }

    public virtual ActivitySchedule Activity { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
