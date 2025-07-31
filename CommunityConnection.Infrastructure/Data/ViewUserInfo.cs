using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class ViewUserInfo
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string? DescriptionSkill { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? GoalName { get; set; }

    public int? Role { get; set; }

    public string? CommunityName { get; set; }

    public string? FieldName { get; set; }
}
