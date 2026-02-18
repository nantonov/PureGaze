using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Subtopics.UpdateSubtopic;

public sealed record UpdateSubtopicCommand(
    int Id,
    List<UpdateSubtopicTranslateDto> Translates) : IRequest;

public sealed record UpdateSubtopicTranslateDto(Language Language, string Name);
