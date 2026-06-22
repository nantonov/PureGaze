using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Questions.UpdateQuestion;

public record UpdateQuestionCommand(
    int Id,
    List<UpdateQuestionTranslateDto> Translates,
    UpdateQuestionAnswerDto Answer) : IRequest
{
    public static void Update(Question question, UpdateQuestionCommand command)
    {
        foreach (UpdateQuestionTranslateDto translate in command.Translates)
        {
            TranslationSync.Update(
                question.QuestionTranslates,
                translate.Language,
                current => current.Content = translate.Content,
                language => new QuestionTranslate
                {
                    QuestionId = question.Id,
                    Language = language,
                    Content = translate.Content
                },
                current => current.Language == translate.Language);
        }

        foreach (UpdateQuestionAnswerTranslateDto translate in command.Answer.Translates)
        {
            if (question.Answer is null)
                continue;

            TranslationSync.Update(
                question.Answer.AnswerTranslates,
                translate.Language,
                current => current.Content = translate.Content,
                language => new AnswerTranslate
                {
                    AnswerId = question.Answer.Id,
                    Language = language,
                    Content = translate.Content
                },
                current => current.Language == translate.Language);
        }
    }
}

public record UpdateQuestionTranslateDto(Language Language, string Content);

public record UpdateQuestionAnswerDto(List<UpdateQuestionAnswerTranslateDto> Translates);

public record UpdateQuestionAnswerTranslateDto(Language Language, string Content);
