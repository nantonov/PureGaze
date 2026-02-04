using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Content.Subtopics.UpdateSubtopic;

public record UpdateSubtopicCommand(
    int Id,
    List<UpdateSubtopicTranslateDto> Translates) : IRequest;

public record UpdateSubtopicTranslateDto(Language Language, string Name);
