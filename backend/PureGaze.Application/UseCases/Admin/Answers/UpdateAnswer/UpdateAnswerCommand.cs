using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Answers.UpdateAnswer;

public record UpdateAnswerCommand(int Id, List<UpdateAnswerTranslateDto> Translates) : IRequest
{
    public static void Apply(Answer answer, UpdateAnswerCommand command)
    {
        foreach (UpdateAnswerTranslateDto translate in command.Translates)
        {
            TranslationSync.Update(
                answer.AnswerTranslates,
                translate.Language,
                current => current.Content = translate.Content,
                language => new AnswerTranslate
                {
                    AnswerId = answer.Id,
                    Language = language,
                    Content = translate.Content
                },
                current => current.Language == translate.Language);
        }
    }
}

public sealed record UpdateAnswerTranslateDto(Language Language, string Content);
