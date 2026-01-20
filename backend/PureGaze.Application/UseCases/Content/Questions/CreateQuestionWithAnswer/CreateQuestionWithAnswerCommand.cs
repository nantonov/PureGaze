using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Content.Questions.CreateQuestionWithAnswer;

public record CreateQuestionWithAnswerCommand(
    int SubTopicId,
    List<CreateQuestionTranslateDto> Translates,
    CreateQuestionAnswerDto Answer) : IRequest<int>;

public record CreateQuestionTranslateDto(Language Language, string Content);

public record CreateQuestionAnswerDto(List<CreateAnswerTranslateDto> Translates);

public record CreateAnswerTranslateDto(Language Language, string Content);
