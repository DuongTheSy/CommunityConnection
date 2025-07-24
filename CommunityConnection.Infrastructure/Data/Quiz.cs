using System;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Data;

public partial class Quiz
{
    public long Id { get; set; }

    public long? ChannelId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Description { get; set; }

    public int? DurationMinutes { get; set; }

    public string? QuizType { get; set; }

    public virtual Channel? Channel { get; set; }

    public virtual ICollection<LevelQuiz> LevelQuizzes { get; set; } = new List<LevelQuiz>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
}
