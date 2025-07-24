using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class QuizAttemptDetail
{
    public long ResultId { get; set; }

    public long OptionId { get; set; }

    public bool? Status { get; set; }

    public virtual QuestionOption Option { get; set; } = null!;

    public virtual QuizAttempt Result { get; set; } = null!;
}
