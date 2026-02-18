using PureGaze.Domain.Entities;
using PureGaze.Application.Abstractions.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PureGaze.Infrastructure.Database.Repositories;

public class QuestionRepository(AppDbContext context) : IQuestionRepository
{
    public async Task<Question?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.Questions
            .Include(q => q.QuestionTranslates)
            .Include(q => q.Answer!)
                .ThenInclude(a => a.AnswerTranslates)
            .AsSplitQuery()
            .FirstOrDefaultAsync(q => q.Id == id, ct);
    
    public Task<List<Question>> GetBySubTopicIdAsync(int subTopicId, CancellationToken ct = default)
        => context.Questions
            .Include(q => q.QuestionTranslates)
            .Where(q => q.SubTopicId == subTopicId)
            .ToListAsync(ct);

    public async Task AddAsync(Question question, CancellationToken ct = default)
        => await context.Questions.AddAsync(question, ct);

    public void Delete(Question question) 
        => context.Questions.Remove(question);
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
