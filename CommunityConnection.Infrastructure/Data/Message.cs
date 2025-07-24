using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Message
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ChannelId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? SentAt { get; set; }

    public virtual Channel Channel { get; set; } = null!;

    public virtual ICollection<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();

    public virtual User User { get; set; } = null!;
}
