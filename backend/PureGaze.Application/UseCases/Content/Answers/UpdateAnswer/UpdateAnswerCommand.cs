using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Content.Answers.UpdateAnswer;

public record UpdateAnswerCommand(
    int Id,
    List<UpdateAnswerTranslateDto> Translates) : IRequest;

public record UpdateAnswerTranslateDto(Language Language, string Content);
