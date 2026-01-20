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
        var subtopic = await subtopicRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {command.Id} not found.");

        var newNames = command.Translates
            .Where(tDto => !subtopic.SubtopicTranslates.Any(st => st.Language == tDto.Language && st.Name == tDto.Name))
            .Select(tDto => tDto.Name)
            .Distinct()
            .ToList();

        if (newNames.Any())
        {
            var existingName = await subtopicRepository.GetAnyExistingNameAsync(subtopic.TopicId, newNames, subtopic.Id, ct);
            if (existingName != null)
            {
                throw new ValidationException($"Subtopic with name '{existingName}' already exists in topic '{subtopic.TopicId}'.");
            }
        }

        foreach (var translateDto in command.Translates)
        {
            subtopic.SubtopicTranslates.SyncTranslate(
                translateDto.Language,
                t => t.Name = translateDto.Name,
                lang => new SubtopicTranslate { SubtopicId = subtopic.Id, Language = lang, Name = translateDto.Name },
                t => t.Language == translateDto.Language);
        }

        await subtopicRepository.SaveChangesAsync(ct);
    }
}
