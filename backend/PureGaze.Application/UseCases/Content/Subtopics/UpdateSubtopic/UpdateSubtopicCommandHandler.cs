using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Subtopics.UpdateSubtopic;

public class UpdateSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<UpdateSubtopicCommand>
{
    public async Task Handle(UpdateSubtopicCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);

        var subtopic = await subtopicRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {command.Id} not found.");

        await ValidateUniquenessAsync(subtopic, command.Translates, ct);

        subtopic.Update(command.Translates);

        await subtopicRepository.SaveChangesAsync(ct);
    }

    private void ValidateInput(UpdateSubtopicCommand command)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);

        if (command.Translates.Count == 0)
            throw new ArgumentException("At least one subtopic translate is required.");
    }

    private async Task ValidateUniquenessAsync(Subtopic subtopic, List<UpdateSubtopicTranslateDto> translates, CancellationToken ct)
    {
        var newNames = translates
            .Where(tDto => !subtopic.SubtopicTranslates.Any(st => st.Language == tDto.Language && st.Name == tDto.Name))
            .Select(tDto => tDto.Name)
            .Distinct()
            .ToList();

        if (newNames.Count > 0)
        {
            var existingName = await subtopicRepository.GetAnyExistingNameAsync(subtopic.TopicId, newNames, subtopic.Id, ct);
            if (existingName != null)
            {
                throw new ValidationException($"Subtopic with name '{existingName}' already exists in topic '{subtopic.TopicId}'.");
            }
        }
    }
}
