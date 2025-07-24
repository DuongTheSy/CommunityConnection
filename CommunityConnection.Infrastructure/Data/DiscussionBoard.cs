using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class DiscussionBoard
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? Status { get; set; }

    public long CreatorUserId { get; set; }

    public long? ChannelId { get; set; }

    public virtual Channel? Channel { get; set; }

    public virtual User CreatorUser { get; set; } = null!;

    public virtual ICollection<DiscussionComment> DiscussionComments { get; set; } = new List<DiscussionComment>();
}
