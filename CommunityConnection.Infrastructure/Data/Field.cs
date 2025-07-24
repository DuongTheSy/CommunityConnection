using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Field
{
    public long Id { get; set; }

    public string FieldName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
