using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PureGaze.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<ManagerialLevel> ManagerialLevels { get; set; }
    public DbSet<ProfessionalLevel> ProfessionalLevels { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<AssessmentRequest> AssessmentRequests { get; set; }
    public DbSet<AssessmentStage> AssessmentStages { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Subtopic> Subtopics { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Code> Codes { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<SubtopicScore> SubtopicScores { get; set; }
    public DbSet<TopicTranslate> TopicTranslates { get; set; }
    public DbSet<SubtopicTranslate> SubtopicTranslates { get; set; }
    public DbSet<QuestionTranslate> QuestionTranslates { get; set; }
    public DbSet<AnswerTranslate> AnswerTranslates { get; set; }
}