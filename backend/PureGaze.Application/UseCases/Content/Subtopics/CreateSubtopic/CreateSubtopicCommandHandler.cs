using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Subtopics.CreateSubtopic;

public class CreateSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<CreateSubtopicCommand>
{
    public async Task Handle(CreateSubtopicCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);
        await ValidateUniquenessAsync(command, ct);
        
        await subtopicRepository.AddAsync(command.ToEntity(), ct);
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
        var names = command.Translates.Select(t => t.Name);
        
        if (await subtopicRepository.IsNameExistingAsync(command.TopicId, names, null, ct)) 
            throw new ValidationException($"Subtopic with one of names already exists in topic '{command.TopicId}'.");
        
    }
}
