using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class QuestionOption
{
    public long Id { get; set; }

    public long QuestionId { get; set; }

    public string Content { get; set; } = null!;

    public bool? IsCorrect { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<QuizAttemptDetail> QuizAttemptDetails { get; set; } = new List<QuizAttemptDetail>();
}
