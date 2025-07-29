using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class GoalNote
{
    public long Id { get; set; }

    public long GoalId { get; set; }

    public string NoteText { get; set; } = null!;

    public int? OrderIndex { get; set; }

    public virtual Goal Goal { get; set; } = null!;
}
