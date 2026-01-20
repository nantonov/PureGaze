using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Subtopics.DeleteSubtopic;

public class DeleteSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<DeleteSubtopicCommand>
{
    public async Task Handle(DeleteSubtopicCommand command, CancellationToken ct = default)
    {
        var subtopic = await subtopicRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {command.Id} not found.");

        await subtopicRepository.DeleteAsync(subtopic, ct);
        await subtopicRepository.SaveChangesAsync(ct);
    }
}
