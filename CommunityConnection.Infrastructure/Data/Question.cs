using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Question
{
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public string? DifficultyLevel { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long QuizId { get; set; }

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual Quiz Quiz { get; set; } = null!;
}
