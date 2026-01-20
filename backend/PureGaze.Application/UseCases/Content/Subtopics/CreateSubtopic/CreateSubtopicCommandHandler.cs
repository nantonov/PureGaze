using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Subtopics.CreateSubtopic;

public class CreateSubtopicCommandHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<CreateSubtopicCommand>
{
    public async Task Handle(CreateSubtopicCommand command, CancellationToken ct = default)
    {
        var names = command.Translates.Select(t => t.Name).ToList();
        var existingName = await subtopicRepository.GetAnyExistingNameAsync(command.TopicId, names, null, ct);
        
        if (existingName != null)
        {
            throw new ValidationException($"Subtopic with name '{existingName}' already exists in topic '{command.TopicId}'.");
        }

        var subtopic = new Subtopic
        {
            TopicId = command.TopicId,
            SubtopicTranslates = command.Translates.Select(t => new SubtopicTranslate
            {
                Language = t.Language,
                Name = t.Name
            }).ToList(),
            Questions = command.Questions.Select(qDto => new Question
            {
                QuestionTranslates = qDto.Translates.Select(t => new QuestionTranslate
                {
                    Language = t.Language,
                    Content = t.Content
                }).ToList(),
                Answer = new Answer
                {
                    AnswerTranslates = qDto.Answer.Translates.Select(t => new AnswerTranslate
                    {
                        Language = t.Language,
                        Content = t.Content
                    }).ToList()
                }
            }).ToList()
        };

        await subtopicRepository.AddAsync(subtopic, ct);
        await subtopicRepository.SaveChangesAsync(ct);
    }
}
