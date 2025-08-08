using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? DescriptionSkill { get; set; }

    public long? RoleId { get; set; }

    public string? Address { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? Token { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthdate { get; set; }

    public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();

    public virtual ICollection<ActivitySchedule> ActivitySchedules { get; set; } = new List<ActivitySchedule>();

    public virtual ICollection<ChannelMember> ChannelMembers { get; set; } = new List<ChannelMember>();

    public virtual ICollection<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();

    public virtual ICollection<CommunityContribution> CommunityContributions { get; set; } = new List<CommunityContribution>();

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

    public virtual ICollection<ConnectionRequest> ConnectionRequestReceiverUsers { get; set; } = new List<ConnectionRequest>();

    public virtual ICollection<ConnectionRequest> ConnectionRequestSenderUsers { get; set; } = new List<ConnectionRequest>();

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    public virtual ICollection<JoinRequest> JoinRequestReceiverUsers { get; set; } = new List<JoinRequest>();

    public virtual ICollection<JoinRequest> JoinRequestSenderUsers { get; set; } = new List<JoinRequest>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<ReminderNotification> ReminderNotifications { get; set; } = new List<ReminderNotification>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserDevice> UserDevices { get; set; } = new List<UserDevice>();

    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();
}
