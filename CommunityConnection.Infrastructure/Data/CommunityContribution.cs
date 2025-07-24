using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class CommunityContribution
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long CommunityId { get; set; }

    public int? Score { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Community Community { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
