using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class CommunityMember
{
    public long CommunityId { get; set; }

    public long UserId { get; set; }

    public int? Role { get; set; }

    public virtual Community Community { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
