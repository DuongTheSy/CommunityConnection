using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class ReminderNotification
{
    public long Id { get; set; }

    public long? ActivityId { get; set; }

    public long UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime SendTime { get; set; }

    public bool? IsRead { get; set; }

    public virtual ActivitySchedule? Activity { get; set; }

    public virtual User User { get; set; } = null!;
}
