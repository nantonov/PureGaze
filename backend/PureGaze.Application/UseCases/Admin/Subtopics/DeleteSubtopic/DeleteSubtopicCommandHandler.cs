using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Subtopics.DeleteSubtopic;

public class DeleteSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<DeleteSubtopicCommand>
{
    public async Task Handle(DeleteSubtopicCommand command, CancellationToken ct = default)
    {
        Subtopic subtopic = await subtopicRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {command.Id} not found.");

        subtopicRepository.Delete(subtopic);
        await subtopicRepository.SaveChangesAsync(ct);
    }
}
