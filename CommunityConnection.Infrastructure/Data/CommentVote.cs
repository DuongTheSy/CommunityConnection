using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class CommentVote
{
    public long Id { get; set; }

    public long? MessageId { get; set; }

    public long? DiscussionCommentId { get; set; }

    public long UserId { get; set; }

    public string VoteType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual DiscussionComment? DiscussionComment { get; set; }

    public virtual Message? Message { get; set; }

    public virtual User User { get; set; } = null!;
}
