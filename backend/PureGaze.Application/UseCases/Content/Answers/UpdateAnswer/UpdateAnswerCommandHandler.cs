using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Answers.UpdateAnswer;

public class UpdateAnswerCommandHandler(IAnswerRepository answerRepository)
    : IRequestHandler<UpdateAnswerCommand>
{
    public async Task Handle(UpdateAnswerCommand command, CancellationToken ct = default)
    {
        var answer = await answerRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Answer with Id {command.Id} not found.");

        foreach (var translateDto in command.Translates)
        {
            answer.AnswerTranslates.SyncTranslate(
                translateDto.Language,
                t => t.Content = translateDto.Content,
                lang => new AnswerTranslate { AnswerId = answer.Id, Language = lang, Content = translateDto.Content },
                t => t.Language == translateDto.Language);
        }

        await answerRepository.SaveChangesAsync(ct);
    }
}
