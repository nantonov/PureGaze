using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Subtopics.CreateSubtopic;

public class CreateSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<CreateSubtopicCommand, CreateSubtopicResult>
{
    public async Task<CreateSubtopicResult> Handle(CreateSubtopicCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);
        await ValidateUniquenessAsync(command, ct);

        Subtopic entity = CreateSubtopicCommand.ToEntity(command);
        await subtopicRepository.AddAsync(entity, ct);
        await subtopicRepository.SaveChangesAsync(ct);

        return new CreateSubtopicResult { SubtopicId = entity.Id };
    }

    private void ValidateInput(CreateSubtopicCommand command)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);

        if (command.Translates.Count == 0)
            throw new ArgumentException("At least one subtopic translate is required.");
    }

    private async Task ValidateUniquenessAsync(CreateSubtopicCommand command, CancellationToken ct)
    {
        IEnumerable<string> names = command.Translates.Select(t => t.Name);

        if (await subtopicRepository.IsNameExistingAsync(command.TopicId, names, null, ct))
            throw new ValidationException($"Subtopic with one of names already exists in topic '{command.TopicId}'.");

    }
}
