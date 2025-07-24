using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class DiscussionComment
{
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? HelpfulVotes { get; set; }

    public int? UnhelpfulVotes { get; set; }

    public long BoardId { get; set; }

    public virtual DiscussionBoard Board { get; set; } = null!;

    public virtual ICollection<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();
}
