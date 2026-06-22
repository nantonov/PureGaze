using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Questions.DeleteQuestion;

public class DeleteQuestionCommandHandler(IQuestionRepository questionRepository)
    : IRequestHandler<DeleteQuestionCommand>
{
    public async Task Handle(DeleteQuestionCommand command, CancellationToken ct = default)
    {
        Question question = await questionRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Question with Id {command.Id} not found.");

        questionRepository.Delete(question);
        await questionRepository.SaveChangesAsync(ct);
    }
}
