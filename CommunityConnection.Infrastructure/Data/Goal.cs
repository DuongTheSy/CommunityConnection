using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Goal
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string GoalName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int? Status { get; set; }

    public string? PriorityLevel { get; set; }

    public virtual ICollection<GoalNote> GoalNotes { get; set; } = new List<GoalNote>();

    public virtual ICollection<Method> Methods { get; set; } = new List<Method>();

    public virtual ICollection<SubGoal> SubGoals { get; set; } = new List<SubGoal>();

    public virtual User User { get; set; } = null!;
}
