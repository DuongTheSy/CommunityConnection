using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class SubGoal
{
    public long Id { get; set; }

    public long GoalId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? OrderIndex { get; set; }

    public DateTime? CompletionDate { get; set; }

    public string? Activity { get; set; }

    public virtual Goal Goal { get; set; } = null!;
}
