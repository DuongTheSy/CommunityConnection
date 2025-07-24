using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class ConnectionRequest
{
    public long Id { get; set; }

    public string? Message { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? ResponseMessage { get; set; }

    public long SenderUserId { get; set; }

    public long ReceiverUserId { get; set; }

    public virtual User ReceiverUser { get; set; } = null!;

    public virtual User SenderUser { get; set; } = null!;
}
