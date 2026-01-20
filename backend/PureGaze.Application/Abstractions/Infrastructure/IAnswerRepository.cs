using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAnswerRepository
{
    Task<Answer?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Answer?> GetByQuestionIdAsync(int questionId, CancellationToken ct = default);
    Task AddAsync(Answer answer, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
