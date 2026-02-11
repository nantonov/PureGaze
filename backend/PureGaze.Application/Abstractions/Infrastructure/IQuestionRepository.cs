using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IQuestionRepository
{
    Task<Question?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<Question>> GetBySubTopicIdAsync(int subTopicId, CancellationToken ct = default);
    Task AddAsync(Question question, CancellationToken ct = default);
    void Delete(Question question);
    Task SaveChangesAsync(CancellationToken ct = default);
}
