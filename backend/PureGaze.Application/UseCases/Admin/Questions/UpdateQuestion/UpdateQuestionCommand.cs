using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Questions.UpdateQuestion;

public record UpdateQuestionCommand(
    int Id,
    List<UpdateQuestionTranslateDto> Translates,
    UpdateQuestionAnswerDto Answer) : IRequest;

public record UpdateQuestionTranslateDto(Language Language, string Content);

public record UpdateQuestionAnswerDto(List<UpdateQuestionAnswerTranslateDto> Translates);

public record UpdateQuestionAnswerTranslateDto(Language Language, string Content);
