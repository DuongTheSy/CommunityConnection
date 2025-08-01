using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnection.Infrastructure.Data;

public partial class ThesisContext : DbContext
{
    public ThesisContext()
    {
    }

    public ThesisContext(DbContextOptions<ThesisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<ActivitySchedule> ActivitySchedules { get; set; }

    public virtual DbSet<ActivitySession> ActivitySessions { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<ChannelMember> ChannelMembers { get; set; }

    public virtual DbSet<CommentVote> CommentVotes { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<CommunityContribution> CommunityContributions { get; set; }

    public virtual DbSet<CommunityMember> CommunityMembers { get; set; }

    public virtual DbSet<ConnectionRequest> ConnectionRequests { get; set; }

    public virtual DbSet<DiscussionBoard> DiscussionBoards { get; set; }

    public virtual DbSet<DiscussionComment> DiscussionComments { get; set; }

    public virtual DbSet<Drawing> Drawings { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<GoalNote> GoalNotes { get; set; }

    public virtual DbSet<JoinRequest> JoinRequests { get; set; }

    public virtual DbSet<LevelQuiz> LevelQuizzes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Method> Methods { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizAttempt> QuizAttempts { get; set; }

    public virtual DbSet<QuizAttemptDetail> QuizAttemptDetails { get; set; }

    public virtual DbSet<ReminderNotification> ReminderNotifications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubGoal> SubGoals { get; set; }

    public virtual DbSet<SubGoalActivity> SubGoalActivities { get; set; }

    public virtual DbSet<TextNote> TextNotes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewUserInfo> ViewUserInfos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=duongthesy.database.windows.net;Initial Catalog=Thesis;Persist Security Info=True;User ID=duongthesy;Password=Duongsy1201@;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__achievem__3213E83F736E2360");

            entity.ToTable("achievement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Streak)
                .HasDefaultValue(0)
                .HasColumnName("streak");
            entity.Property(e => e.UpdateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Achievements)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_achievement_user");
        });

        modelBuilder.Entity<ActivitySchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__activity__3213E83FA29211DF");

            entity.ToTable("activity_schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityName)
                .HasMaxLength(255)
                .HasColumnName("activity_name");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndTime)
                .HasMaxLength(50)
                .HasColumnName("end_time");
            entity.Property(e => e.StartTime)
                .HasMaxLength(50)
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ActivitySchedules)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_activity_schedule_user");
        });

        modelBuilder.Entity<ActivitySession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__activity__69B13FDC406BF182");

            entity.ToTable("activity_session");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivitySessions)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity_session_activity");

            entity.HasOne(d => d.User).WithMany(p => p.ActivitySessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity_session_user");
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__channel__3213E83FB7A1F610");

            entity.ToTable("channel");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChannelName)
                .HasMaxLength(255)
                .HasColumnName("channel_name");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.Community).WithMany(p => p.Channels)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("fk_channel_community");
        });

        modelBuilder.Entity<ChannelMember>(entity =>
        {
            entity.HasKey(e => new { e.ChannelId, e.UserId }).HasName("PK__channel___C69382DBA5EF7E12");

            entity.ToTable("channel_member");

            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role)
                .HasDefaultValue(1)
                .HasColumnName("role");

            entity.HasOne(d => d.Channel).WithMany(p => p.ChannelMembers)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("fk_channel_member_channel");

            entity.HasOne(d => d.User).WithMany(p => p.ChannelMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_channel_member_user");
        });

        modelBuilder.Entity<CommentVote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comment___3213E83F2A150D05");

            entity.ToTable("comment_vote");

            entity.HasIndex(e => new { e.UserId, e.DiscussionCommentId }, "uq_user_vote_comment").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.MessageId }, "uq_user_vote_message").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscussionCommentId).HasColumnName("discussion_comment_id");
            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VoteType)
                .HasMaxLength(20)
                .HasColumnName("vote_type");

            entity.HasOne(d => d.DiscussionComment).WithMany(p => p.CommentVotes)
                .HasForeignKey(d => d.DiscussionCommentId)
                .HasConstraintName("fk_comment_vote_comment");

            entity.HasOne(d => d.Message).WithMany(p => p.CommentVotes)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("fk_comment_vote_message");

            entity.HasOne(d => d.User).WithMany(p => p.CommentVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comment_vote_user");
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__communit__3213E83F858F748C");

            entity.ToTable("community");

            entity.HasIndex(e => e.CommunityName, "UQ__communit__F712701929B00ADF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessStatus)
                .HasDefaultValue(1)
                .HasColumnName("access_status");
            entity.Property(e => e.CommunityName)
                .HasMaxLength(255)
                .HasColumnName("community_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.MemberCount)
                .HasDefaultValue(0)
                .HasColumnName("member_count");
            entity.Property(e => e.SkillLevel)
                .HasMaxLength(50)
                .HasColumnName("skill_level");
        });

        modelBuilder.Entity<CommunityContribution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__communit__3213E83F4C3829AE");

            entity.ToTable("community_contribution");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.Score)
                .HasDefaultValue(0)
                .HasColumnName("score");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Community).WithMany(p => p.CommunityContributions)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("fk_comm_contrib_community");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityContributions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_comm_contrib_user");
        });

        modelBuilder.Entity<CommunityMember>(entity =>
        {
            entity.HasKey(e => new { e.CommunityId, e.UserId }).HasName("PK__communit__D18390EF30A29D07");

            entity.ToTable("community_member");

            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role)
                .HasDefaultValue(1)
                .HasColumnName("role");

            entity.HasOne(d => d.Community).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("fk_comm_member_community");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_comm_member_user");
        });

        modelBuilder.Entity<ConnectionRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__connecti__3213E83FCA5FB149");

            entity.ToTable("connection_request");

            entity.HasIndex(e => new { e.SenderUserId, e.ReceiverUserId }, "uq_connection_request").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.ReceiverUserId).HasColumnName("receiver_user_id");
            entity.Property(e => e.ResponseMessage).HasColumnName("response_message");
            entity.Property(e => e.SenderUserId).HasColumnName("sender_user_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ReceiverUser).WithMany(p => p.ConnectionRequestReceiverUsers)
                .HasForeignKey(d => d.ReceiverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_conn_req_receiver");

            entity.HasOne(d => d.SenderUser).WithMany(p => p.ConnectionRequestSenderUsers)
                .HasForeignKey(d => d.SenderUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_conn_req_sender");
        });

        modelBuilder.Entity<DiscussionBoard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__discussi__3213E83F1BD1CB7C");

            entity.ToTable("discussion_board");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatorUserId).HasColumnName("creator_user_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Channel).WithMany(p => p.DiscussionBoards)
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_discussion_board_channel");

            entity.HasOne(d => d.CreatorUser).WithMany(p => p.DiscussionBoards)
                .HasForeignKey(d => d.CreatorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_discussion_board_creator");
        });

        modelBuilder.Entity<DiscussionComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__discussi__3213E83F07E0D0A2");

            entity.ToTable("discussion_comment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BoardId).HasColumnName("board_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.HelpfulVotes)
                .HasDefaultValue(0)
                .HasColumnName("helpful_votes");
            entity.Property(e => e.UnhelpfulVotes)
                .HasDefaultValue(0)
                .HasColumnName("unhelpful_votes");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Board).WithMany(p => p.DiscussionComments)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("fk_discussion_comment_board");
        });

        modelBuilder.Entity<Drawing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__drawing__3213E83F645339C1");

            entity.ToTable("drawing");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Drawings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_drawing_user");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__field__3213E83FEE77F402");

            entity.ToTable("field");

            entity.HasIndex(e => e.FieldName, "UQ__field__7EFADAB9DAFE578D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FieldName)
                .HasMaxLength(100)
                .HasColumnName("field_name");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__goal__3213E83FBB88CF4A");

            entity.ToTable("goal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompletionDate)
                .HasColumnType("datetime")
                .HasColumnName("completion_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GoalName)
                .HasMaxLength(255)
                .HasColumnName("goal_name");
            entity.Property(e => e.PriorityLevel)
                .HasMaxLength(20)
                .HasDefaultValue("medium")
                .HasColumnName("priority_level");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Goals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_goal_user");
        });

        modelBuilder.Entity<GoalNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__goal_not__3213E83F6C257FDE");

            entity.ToTable("goal_note");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GoalId).HasColumnName("goal_id");
            entity.Property(e => e.NoteText).HasColumnName("note_text");
            entity.Property(e => e.OrderIndex)
                .HasDefaultValue(0)
                .HasColumnName("order_index");

            entity.HasOne(d => d.Goal).WithMany(p => p.GoalNotes)
                .HasForeignKey(d => d.GoalId)
                .HasConstraintName("fk_goal_note");
        });

        modelBuilder.Entity<JoinRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__join_req__3213E83FDBD64109");

            entity.ToTable("join_request");

            entity.HasIndex(e => new { e.SenderUserId, e.CommunityId }, "uq_join_request").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.ReceiverUserId).HasColumnName("receiver_user_id");
            entity.Property(e => e.ResponseMessage).HasColumnName("response_message");
            entity.Property(e => e.SenderUserId).HasColumnName("sender_user_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Community).WithMany(p => p.JoinRequests)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("fk_join_req_community");

            entity.HasOne(d => d.ReceiverUser).WithMany(p => p.JoinRequestReceiverUsers)
                .HasForeignKey(d => d.ReceiverUserId)
                .HasConstraintName("fk_join_req_receiver");

            entity.HasOne(d => d.SenderUser).WithMany(p => p.JoinRequestSenderUsers)
                .HasForeignKey(d => d.SenderUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_join_req_sender");
        });

        modelBuilder.Entity<LevelQuiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__level_qu__3213E83FB299A4CB");

            entity.ToTable("level_quiz");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LevelName)
                .HasMaxLength(100)
                .HasColumnName("level_name");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.ScoreLower).HasColumnName("score_lower");
            entity.Property(e => e.ScoreUpper).HasColumnName("score_upper");

            entity.HasOne(d => d.Quiz).WithMany(p => p.LevelQuizzes)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("fk_level_quiz_quiz");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__message__3213E83F11297D0C");

            entity.ToTable("message");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sent_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Channel).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("fk_message_channel");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_message_user");
        });

        modelBuilder.Entity<Method>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__method__3213E83F0456D650");

            entity.ToTable("method");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GoalId).HasColumnName("goal_id");
            entity.Property(e => e.MethodName)
                .HasMaxLength(255)
                .HasColumnName("method_name");
            entity.Property(e => e.Roadmap).HasColumnName("roadmap");
            entity.Property(e => e.Step).HasColumnName("step");

            entity.HasOne(d => d.Goal).WithMany(p => p.Methods)
                .HasForeignKey(d => d.GoalId)
                .HasConstraintName("fk_method_goal");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__question__3213E83FD0D871BF");

            entity.ToTable("question");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(50)
                .HasColumnName("difficulty_level");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("fk_question_quiz");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__question__3213E83F8D04A1EF");

            entity.ToTable("question_option");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsCorrect)
                .HasDefaultValue(false)
                .HasColumnName("is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("fk_question_option_question");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__quiz__3213E83FED551EFF");

            entity.ToTable("quiz");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.QuizType)
                .HasMaxLength(50)
                .HasColumnName("quiz_type");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasDefaultValue(false)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Channel).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_quiz_channel");
        });

        modelBuilder.Entity<QuizAttempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__quiz_att__3213E83FB4107411");

            entity.ToTable("quiz_attempt");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("score");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("fk_quiz_attempt_quiz");

            entity.HasOne(d => d.User).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_quiz_attempt_user");
        });

        modelBuilder.Entity<QuizAttemptDetail>(entity =>
        {
            entity.HasKey(e => new { e.ResultId, e.OptionId }).HasName("PK__quiz_att__00FD6FF7DB0CBF60");

            entity.ToTable("quiz_attempt_detail");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.OptionId).HasColumnName("option_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Option).WithMany(p => p.QuizAttemptDetails)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quiz_attempt_detail_option");

            entity.HasOne(d => d.Result).WithMany(p => p.QuizAttemptDetails)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_quiz_attempt_detail_result");
        });

        modelBuilder.Entity<ReminderNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reminder__3213E83FD5A26DD3");

            entity.ToTable("reminder_notification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.SendTime)
                .HasColumnType("datetime")
                .HasColumnName("send_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Activity).WithMany(p => p.ReminderNotifications)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("fk_reminder_activity");

            entity.HasOne(d => d.User).WithMany(p => p.ReminderNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reminder_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F4E6798E0");

            entity.ToTable("role");

            entity.HasIndex(e => e.RoleName, "UQ__role__783254B14A4DBE9E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SubGoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_goal__3213E83F6AED79D7");

            entity.ToTable("sub_goal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompletionDate)
                .HasColumnType("datetime")
                .HasColumnName("completion_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GoalId).HasColumnName("goal_id");
            entity.Property(e => e.OrderIndex)
                .HasDefaultValue(0)
                .HasColumnName("order_index");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Goal).WithMany(p => p.SubGoals)
                .HasForeignKey(d => d.GoalId)
                .HasConstraintName("fk_sub_goal");
        });

        modelBuilder.Entity<SubGoalActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_goal__3213E83F51BE4D81");

            entity.ToTable("sub_goal_activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activity).HasColumnName("activity");
            entity.Property(e => e.IsCompleted)
                .HasDefaultValue(false)
                .HasColumnName("is_completed");
            entity.Property(e => e.OrderIndex)
                .HasDefaultValue(0)
                .HasColumnName("order_index");
            entity.Property(e => e.SubGoalId).HasColumnName("sub_goal_id");

            entity.HasOne(d => d.SubGoal).WithMany(p => p.SubGoalActivities)
                .HasForeignKey(d => d.SubGoalId)
                .HasConstraintName("fk_activity_sub_goal");
        });

        modelBuilder.Entity<TextNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__text_not__3213E83FE2C90D11");

            entity.ToTable("text_note");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BackgroundColor)
                .HasMaxLength(20)
                .HasColumnName("background_color");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TextColor)
                .HasMaxLength(20)
                .HasColumnName("text_color");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TextNotes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_text_note_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83FE7C2DF75");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E61644FBC8364").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__user__F3DBC57206FA280B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionSkill).HasColumnName("description_skill");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_user_role");

            entity.HasMany(d => d.Fields).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserField",
                    r => r.HasOne<Field>().WithMany()
                        .HasForeignKey("FieldId")
                        .HasConstraintName("fk_user_field_field"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_field_user"),
                    j =>
                    {
                        j.HasKey("UserId", "FieldId").HasName("PK__user_fie__4805584C282B6F11");
                        j.ToTable("user_field");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("FieldId").HasColumnName("field_id");
                    });
        });

        modelBuilder.Entity<ViewUserInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_user_info");

            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CommunityName)
                .HasMaxLength(255)
                .HasColumnName("community_name");
            entity.Property(e => e.DescriptionSkill).HasColumnName("description_skill");
            entity.Property(e => e.FieldName)
                .HasMaxLength(100)
                .HasColumnName("field_name");
            entity.Property(e => e.GoalName)
                .HasMaxLength(255)
                .HasColumnName("goal_name");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
