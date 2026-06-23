using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Questions.CreateQuestionWithAnswer;

public record CreateQuestionWithAnswerCommand(
    int SubTopicId,
    List<CreateQuestionTranslateDto> Translates,
    CreateQuestionAnswerDto Answer) : IRequest<int>
{
    public static Question ToEntity(CreateQuestionWithAnswerCommand command)
        => new()
        {
            SubTopicId = command.SubTopicId,
            QuestionTranslates = [.. command.Translates.Select(t => new QuestionTranslate
            {
                Language = t.Language,
                Content = t.Content
            })],
            Answer = new Answer
            {
                AnswerTranslates = [.. command.Answer.Translates.Select(t => new AnswerTranslate
                {
                    Language = t.Language,
                    Content = t.Content
                })]
            }
        };
}

public record CreateQuestionTranslateDto(Language Language, string Content);

public record CreateQuestionAnswerDto(List<CreateAnswerTranslateDto> Translates);

public record CreateAnswerTranslateDto(Language Language, string Content);
