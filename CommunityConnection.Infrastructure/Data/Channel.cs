using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Channel
{
    public long Id { get; set; }

    public long? CommunityId { get; set; }

    public string ChannelName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ChannelMember> ChannelMembers { get; set; } = new List<ChannelMember>();

    public virtual Community? Community { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
