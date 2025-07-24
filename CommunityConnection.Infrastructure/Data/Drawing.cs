using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Drawing
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string? Title { get; set; }

    public string? Data { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
