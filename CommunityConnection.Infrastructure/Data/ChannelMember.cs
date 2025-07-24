using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class ChannelMember
{
    public long ChannelId { get; set; }

    public long UserId { get; set; }

    public int? Role { get; set; }

    public virtual Channel Channel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
