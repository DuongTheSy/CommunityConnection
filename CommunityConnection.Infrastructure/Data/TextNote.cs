using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class TextNote
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? BackgroundColor { get; set; }

    public string? TextColor { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
