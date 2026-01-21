using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Subtopics.CreateSubtopic;

public class CreateSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<CreateSubtopicCommand>
{
    public async Task Handle(CreateSubtopicCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);
        await ValidateUniquenessAsync(command, ct);
        
        var subtopic = command.ToEntity();

        await subtopicRepository.AddAsync(subtopic, ct);
        await subtopicRepository.SaveChangesAsync(ct);
    }

    private void ValidateInput(CreateSubtopicCommand command)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);
        ArgumentNullException.ThrowIfNull(command.Questions);

        if (command.Translates.Count == 0)
            throw new ArgumentException("At least one subtopic translate is required.");
    }
    
    private async Task ValidateUniquenessAsync(CreateSubtopicCommand command, CancellationToken ct)
    {
        // Validate subtopic names uniqueness
        var names = command.Translates.Select(t => t.Name).ToList();
        var existingName = await subtopicRepository.GetAnyExistingNameAsync(command.TopicId, names, null, ct);
        
        if (existingName != null)
        {
            throw new ValidationException($"Subtopic with name '{existingName}' already exists in topic '{command.TopicId}'.");
        }
    }
}
