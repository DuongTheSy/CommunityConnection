using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class UserDevice
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string? DeviceToken { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual User User { get; set; } = null!;
}
