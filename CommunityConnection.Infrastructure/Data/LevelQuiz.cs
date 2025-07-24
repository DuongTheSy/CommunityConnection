using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class LevelQuiz
{
    public long Id { get; set; }

    public long QuizId { get; set; }

    public string LevelName { get; set; } = null!;

    public string? Description { get; set; }

    public int? ScoreUpper { get; set; }

    public int? ScoreLower { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;
}
