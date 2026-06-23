using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class AssessmentStageRepository(AppDbContext context)
    : IAssessmentStageRepository
{
    public async Task<AssessmentStage?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.AssessmentStages
            .Include(x => x.Assessment!).ThenInclude(a => a.Code!).ThenInclude(c => c.ToGrade)
            .Include(x => x.Assessor)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<AssessmentStage?> GetByIdWithScoresAndTopicAsync(int id, CancellationToken ct = default)
        => await context.AssessmentStages
            .Include(x => x.Assessment)
            .Include(x => x.Assessor)
            .Include(x => x.Topic!)
                .ThenInclude(x => x.Subtopics)
            .Include(x => x.Scores)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<bool> HasAssessorInAssessmentAsync(int assessmentId, int assessorId, CancellationToken ct = default)
        => await context.AssessmentStages.AnyAsync(s => s.AssessmentId == assessmentId && s.AssessorId == assessorId, ct);

    public async Task<IReadOnlyList<AssessmentStage>> GetAssignedToAsync(string assessorEmail, CancellationToken ct = default)
        => await context.AssessmentStages
            .AsNoTracking()
            .Where(x => x.Assessor != null && x.Assessor.Email == assessorEmail && x.Status != Domain.Enums.StageStatus.Completed)
            .Include(x => x.Assessment!).ThenInclude(x => x.Employee)
            .Include(x => x.Topic!).ThenInclude(x => x.TopicTranslates)
            .Include(x => x.Topic!).ThenInclude(x => x.Subtopics).ThenInclude(x => x.SubtopicTranslates)
            .Include(x => x.Topic!).ThenInclude(x => x.Subtopics).ThenInclude(x => x.Questions).ThenInclude(x => x.QuestionTranslates)
            .Include(x => x.Topic!).ThenInclude(x => x.Subtopics).ThenInclude(x => x.Questions).ThenInclude(x => x.Answer!).ThenInclude(x => x.AnswerTranslates)
            .Include(x => x.Scores)
            .AsSplitQuery()
            .OrderBy(x => x.Id)
            .ToListAsync(ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
