using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Method
{
    public long Id { get; set; }

    public long GoalId { get; set; }

    public string MethodName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Roadmap { get; set; }

    public string? Step { get; set; }

    public virtual Goal Goal { get; set; } = null!;
}
