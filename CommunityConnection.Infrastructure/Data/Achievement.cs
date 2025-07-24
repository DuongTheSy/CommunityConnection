using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Achievement
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? Streak { get; set; }

    public bool? Status { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
