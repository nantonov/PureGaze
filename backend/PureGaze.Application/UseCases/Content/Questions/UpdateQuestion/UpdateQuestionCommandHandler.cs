using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Questions.UpdateQuestion;

public class UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
    : IRequestHandler<UpdateQuestionCommand>
{
    public async Task Handle(UpdateQuestionCommand command, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);
        ArgumentNullException.ThrowIfNull(command.Answer);
        ArgumentNullException.ThrowIfNull(command.Answer.Translates);

        if (!command.Translates.Any()) throw new ArgumentException("At least one question translate is required.");
        if (!command.Answer.Translates.Any()) throw new ArgumentException("At least one answer translate is required.");

        var question = await questionRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Question with Id {command.Id} not found.");

        foreach (var translateDto in command.Translates)
        {
            question.QuestionTranslates.SyncTranslate(
                translateDto.Language,
                t => t.Content = translateDto.Content,
                lang => new QuestionTranslate { QuestionId = question.Id, Language = lang, Content = translateDto.Content },
                t => t.Language == translateDto.Language);
        }

        foreach (var translateDto in command.Answer.Translates)
        {
            question.Answer.AnswerTranslates.SyncTranslate(
                translateDto.Language,
                t => t.Content = translateDto.Content,
                lang => new AnswerTranslate { AnswerId = question.Answer.Id, Language = lang, Content = translateDto.Content },
                t => t.Language == translateDto.Language);
        }

        await questionRepository.SaveChangesAsync(ct);
    }
}
