using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Answers.UpdateAnswer;

public record UpdateAnswerCommand(int Id, List<UpdateAnswerTranslateDto> Translates) : IRequest;

public sealed record UpdateAnswerTranslateDto(Language Language, string Content);