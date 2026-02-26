using PureGaze.Domain.Entities;
using PureGaze.Application.Abstractions.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PureGaze.Infrastructure.Database.Repositories;

public class AnswerRepository(AppDbContext context) : IAnswerRepository
{
    public async Task<Answer?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.Answers
            .Include(a => a.AnswerTranslates)
            .FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<Answer?> GetByQuestionIdAsync(int questionId, CancellationToken ct = default)
        => await context.Answers
            .Include(a => a.AnswerTranslates)
            .FirstOrDefaultAsync(a => a.QuestionId == questionId, ct);

    public async Task AddAsync(Answer answer, CancellationToken ct = default)
        => await context.Answers.AddAsync(answer, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
