using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Subtopics.UpdateSubtopic;

public class UpdateSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<UpdateSubtopicCommand>
{
    public async Task Handle(UpdateSubtopicCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);

        Subtopic subtopic = await subtopicRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {command.Id} not found.");

        await ValidateUniquenessAsync(subtopic, command.Translates, ct);

        UpdateSubtopicCommand.Apply(subtopic, command);

        await subtopicRepository.SaveChangesAsync(ct);
    }

    private void ValidateInput(UpdateSubtopicCommand command)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);

        if (command.Translates.Count == 0)
            throw new ArgumentException("At least one subtopic translate is required.");
    }

    private async Task ValidateUniquenessAsync(Subtopic subtopic, IReadOnlyList<UpdateSubtopicTranslateDto> translates, CancellationToken ct)
    {
        IReadOnlyList<string> newNames = [.. translates
            .Where(tDto => !subtopic.SubtopicTranslates.Any(st => st.Language == tDto.Language && st.Name == tDto.Name))
            .Select(tDto => tDto.Name)
            .Distinct()];

        if (newNames.Any() && await subtopicRepository.IsNameExistingAsync(subtopic.TopicId, newNames, subtopic.Id, ct))
            throw new ValidationException($"Subtopic with name already exists in topic '{subtopic.TopicId}'.");
    }
}
