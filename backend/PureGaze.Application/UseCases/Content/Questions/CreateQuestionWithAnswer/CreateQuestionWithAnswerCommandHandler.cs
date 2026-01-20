using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Questions.CreateQuestionWithAnswer;

public class CreateQuestionWithAnswerCommandHandler(
    IQuestionRepository questionRepository,
    ISubtopicRepository subtopicRepository)
    : IRequestHandler<CreateQuestionWithAnswerCommand, int>
{
    public async Task<int> Handle(CreateQuestionWithAnswerCommand command, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);
        ArgumentNullException.ThrowIfNull(command.Answer);
        ArgumentNullException.ThrowIfNull(command.Answer.Translates);

        if (!command.Translates.Any()) throw new ArgumentException("At least one question translate is required.");
        if (!command.Answer.Translates.Any()) throw new ArgumentException("At least one answer translate is required.");

        var subtopic = await subtopicRepository.GetByIdAsync(command.SubTopicId, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {command.SubTopicId} not found.");

        var question = new Question
        {
            SubTopicId = command.SubTopicId,
            QuestionTranslates = command.Translates.Select(t => new QuestionTranslate
            {
                Language = t.Language,
                Content = t.Content
            }).ToList(),
            Answer = new Answer
            {
                AnswerTranslates = command.Answer.Translates.Select(t => new AnswerTranslate
                {
                    Language = t.Language,
                    Content = t.Content
                }).ToList()
            }
        };

        await questionRepository.AddAsync(question, ct);
        await questionRepository.SaveChangesAsync(ct);

        return question.Id;
    }
}
