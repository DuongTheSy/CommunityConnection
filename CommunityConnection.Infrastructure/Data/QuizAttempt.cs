using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class QuizAttempt
{
    public long Id { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? DurationMinutes { get; set; }

    public decimal? Score { get; set; }

    public long UserId { get; set; }

    public long QuizId { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<QuizAttemptDetail> QuizAttemptDetails { get; set; } = new List<QuizAttemptDetail>();

    public virtual User User { get; set; } = null!;
}
