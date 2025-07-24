using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Community
{
    public long Id { get; set; }

    public string CommunityName { get; set; } = null!;

    public string? Description { get; set; }

    public int? AccessStatus { get; set; }

    public int? MemberCount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? SkillLevel { get; set; }

    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();

    public virtual ICollection<CommunityContribution> CommunityContributions { get; set; } = new List<CommunityContribution>();

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

    public virtual ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();
}
