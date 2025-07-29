using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class SubGoalActivity
{
    public long Id { get; set; }

    public long SubGoalId { get; set; }

    public string Activity { get; set; } = null!;

    public int? OrderIndex { get; set; }

    public bool? IsCompleted { get; set; }

    public virtual SubGoal SubGoal { get; set; } = null!;
}
