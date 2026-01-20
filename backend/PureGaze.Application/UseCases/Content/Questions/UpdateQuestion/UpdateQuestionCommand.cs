using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Content.Questions.UpdateQuestion;

public record UpdateQuestionCommand(
    int Id,
    List<UpdateQuestionTranslateDto> Translates,
    UpdateQuestionAnswerDto Answer) : IRequest;

public record UpdateQuestionTranslateDto(Language Language, string Content);

public record UpdateQuestionAnswerDto(List<UpdateAnswerTranslateDto> Translates);

public record UpdateAnswerTranslateDto(Language Language, string Content);
