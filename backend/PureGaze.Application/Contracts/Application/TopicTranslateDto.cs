using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public sealed record TopicTranslateDto(
    int TopicId,
    Language Language,
    string? Name);